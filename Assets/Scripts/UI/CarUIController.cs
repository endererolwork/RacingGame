using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

namespace Race
{
    public class CarUIController : MonoBehaviour
    {

        float offsetRotation = 0.0f;
        // Start is called before the first frame update
        void Start()
        {
            offsetRotation = transform.rotation.eulerAngles.z;
        }

        // Update is called once per frame
        void Update()
        {
            float velocity = 0.0f;
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].GetComponent<CarController>().IsLocalPlayer)
                {
                    velocity = Mathf.Abs(players[i].transform.InverseTransformDirection(players[i].GetComponent<Rigidbody>().velocity).z);
                }
            }

            float rotationAngle = Mathf.Lerp(transform.localRotation.z, offsetRotation - (velocity * 3.60f), 1);

            transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

        }
    }
}
