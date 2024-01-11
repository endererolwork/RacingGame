using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{

    public class CarData : MonoBehaviour
    {

        public bool controlable = false;        
        public bool finished = false;        
        public int currentTour = 0;
        public List<float> lapTimes;
        public int currentRanking = 0;

        void Start()
        {
            lapTimes = new List<float>();
        }
    }
}
