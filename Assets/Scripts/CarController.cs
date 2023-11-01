using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;


namespace Race
{
    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor;
        public bool steering;
        public WheelFrictionCurve originalForwardFriction;
        public WheelFrictionCurve originalSidewaysFriction;
    }
    
    public class CarController : MonoBehaviour
    {
        [Header("Axle Information")] 
        [SerializeField] AxleInfo[] axleInfos;

        [Header("Motor Attributes")] 
        [SerializeField]  float maxMotorTorque = 3000f;
        [SerializeField]  float maxSpeed;

        [Header("Steering Attributes")] 
        [SerializeField]  float maxSteeringAngle = 30f;

        [Header("Braking and Drifting Attributes")] 
        [SerializeField]  float brakeTorque = 10000f;
        
        private float brakeVelocity;
        

        [SerializeField]  InputReader input;
        Rigidbody rb;
         void Start()
         {
             rb = GetComponent<Rigidbody>();
             input.Enable();

             foreach (AxleInfo axleInfo in axleInfos)
             {
                 axleInfo.originalForwardFriction = axleInfo.leftWheel.forwardFriction;
                 axleInfo.originalSidewaysFriction = axleInfo.leftWheel.sidewaysFriction;
             }
             
         }

         private void FixedUpdate()
         {
             float verticalInput = AdjustInput(input.Move.y);
             float horizontalInput = AdjustInput(input.Move.x);

             float motor = maxMotorTorque * verticalInput;
             float steering = maxSteeringAngle * horizontalInput;

             UpdateAxles(motor, steering);
         }

         void UpdateAxles(float motor, float steering)
         {
             foreach (AxleInfo axleInfo in axleInfos)
             {
                 HandleSteering(axleInfo, steering);
                 HandleMotor(axleInfo, motor);
                 HandleBreaksAndDrift(axleInfo);
                 UpdateWheelVisiuals(axleInfo.leftWheel);
                 UpdateWheelVisiuals(axleInfo.rightWheel);
             }
         }

         private void UpdateWheelVisiuals(WheelCollider collider)
         {
             if (collider.transform.childCount == 0) return;
             {
                 Transform visualWheel = collider.transform.GetChild(0);

                 Vector3 position;
                 Quaternion rotation;
                 
                 collider.GetWorldPose(out position, out rotation);

                 visualWheel.transform.position = position;
                 visualWheel.transform.rotation = rotation;
             }
         }

         void HandleSteering(AxleInfo axleInfo, float steering)
         {
             if (axleInfo.steering)
             {
                 axleInfo.leftWheel.steerAngle = steering;
                 axleInfo.rightWheel.steerAngle = steering;
             }
         }

         void HandleMotor(AxleInfo axleInfo, float motor)
         {
             if (axleInfo.motor)
             {
                 axleInfo.leftWheel.motorTorque = motor;
                 axleInfo.rightWheel.motorTorque = motor;
             }
         }

         void HandleBreaksAndDrift(AxleInfo axleInfo)
         {
             if (axleInfo.motor)
             {
                 if (input.IsBraking)
                 {
                     rb.constraints = RigidbodyConstraints.FreezePositionX;

                     float newZ = Mathf.SmoothDamp(rb.velocity.z, 0, ref brakeVelocity, 1f);

                     rb.velocity = rb.velocity.With(z: newZ);

                     axleInfo.leftWheel.brakeTorque = 0;
                     axleInfo.rightWheel.brakeTorque = 0;
                 }
                 else
                 {
                     rb.constraints = RigidbodyConstraints.None;

                     axleInfo.leftWheel.brakeTorque = 0;
                     axleInfo.rightWheel.brakeTorque = 0;
                 }


             }
             
         }

         float AdjustInput(float input)
         {
             return input switch
             {
                 >= .7f => 1f,
                 <= -0.7f => -1f,
                 _ => input
             };
         }
    }
}
