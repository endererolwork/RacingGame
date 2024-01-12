using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace Race
{

    public class CarData : NetworkBehaviour
    {

        public bool controlable = false;        
        public bool finished = false;        
        public int currentTour = 0;
        public List<float> lapTimes;
        public int currentRanking = 0;

        public int visualCarId;
        bool first = true;
        void Start()
        {
            lapTimes = new List<float>();
            visualCarId = UnityEngine.Random.Range(0, 3);

        }
        void Update()
        {
            if (first)
            {
                if (IsServer)
                { 
                    if (IsOwner)
                    { 
                        transform.GetChild(0).gameObject.SetActive(true);
                    }
                    else 
                    {
                        transform.GetChild(1).gameObject.SetActive(true);
                    }
                    first = false;
                }
                if (!IsServer)
                {
                    if (IsOwner)
                    {
                        transform.GetChild(1).gameObject.SetActive(true);
                    }
                    else
                    {
                        transform.GetChild(0).gameObject.SetActive(true);
                    }
                    first = false;
                }
            }
        }
    }
}
