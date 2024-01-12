using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Race
{
    public class CheckPointData : MonoBehaviour
    {
        struct GoTimerPair
        {
            public GameObject car;
            public float timer;
        }

        public CheckPointData previousCheckPointData;

        public CheckPointData nextCheckPointData;
        
        public bool firstCheckPoint = false;

        List<List<GoTimerPair>> LapsAndPairs;

        List<List<GameObject>> BlackList;
        // Start is called before the first frame update
        void Start()
        {
            // if (nextCheckPointData == null) { Debug.LogWarning("Check pointlerin ilerisi set edilmemis."); }
            // if (firstCheckPoint && previousCheckPointData == null) { Debug.LogWarning("Check pointlerin gerisi set edilmemis."); }
            LapsAndPairs = new List<List<GoTimerPair>>();
            for( int i =0; i<GameManager.Instance.maxTours; i++)
            {
                LapsAndPairs.Add(new List<GoTimerPair>());
            }
            BlackList = new List<List<GameObject>>();
            for (int i = 0; i < GameManager.Instance.maxTours; i++)
            {
                BlackList.Add(new List<GameObject>());
            }
        }

        public GameObject GetCar(int lap, int i)
        {
            return LapsAndPairs[lap][i].car;
        }

        public float Timer(int lap, int i)
        {
            return LapsAndPairs[lap][i].timer;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (GameManager.Instance.gameEnd) return;

            GameObject go = other.gameObject;
            CarData data = go.GetComponent<CarData>();
            float raceTimer = GameManager.Instance.raceTimer;

            bool skip = false;
            if (go.CompareTag("Player")
                && firstCheckPoint
                && previousCheckPointData.BlackList[data.currentTour].Contains(go)
                && BlackList[data.currentTour].Contains(go))
            {
                data.currentTour++;
                if (data.currentTour > 0)
                {
                    data.lapTimes.Add(raceTimer);
                }
                if (data.currentTour == GameManager.Instance.maxTours)
                {
                    //GameManager.Instance.CarFinished(go, raceTimer);
                    data.finished = true;
                    return;
                }

                BlackList[data.currentTour].Add(go);
                skip = true;
            }


            if (!skip 
                && go.CompareTag("Player") 
                && (!BlackList[data.currentTour].Contains(go))
                && (firstCheckPoint 
                || previousCheckPointData.BlackList[data.currentTour].Contains(go)))
            {
                GoTimerPair goTimerPair;
                goTimerPair.car = go;
                goTimerPair.timer = raceTimer;
                LapsAndPairs[data.currentTour].Add(goTimerPair);
                BlackList[data.currentTour].Add(go);

                int ranking = LapsAndPairs[data.currentTour].Count;
                List<GameObject> onesAboveMe = new List<GameObject>();
                for (int i=0; i< GameManager.Instance.maxTours; i++)
                {
                    if (i > data.currentTour) { onesAboveMe.AddRange(BlackList[i]); }
                    else if (i == data.currentTour) {
                        for (int j=0; j<BlackList[i].Count; j++) 
                        {
                            if (!BlackList[i][j].Equals(go)) { onesAboveMe.Add(BlackList[i][j]); }
                            else { break; }
                        }
                    } // else continue
                }

                data.currentRanking = new HashSet<GameObject>(onesAboveMe).Count + 1;

                Debug.Log(gameObject.name + " -- " + goTimerPair.timer);
            }
        }
    }
}
