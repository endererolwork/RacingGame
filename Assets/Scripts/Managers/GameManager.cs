using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public float raceTimer { get; private set; }

        List<GameObject> ActiveCars; // host is always 0,

        List<CarData> FinishResults; // host is always 0,


        //-- rules below --
        [SerializeField]
        public int maxTours = 3;

        [SerializeField]

        public float startCount = 3.0f;




        // Start is called before the first frame update
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        void Start()
        {
            raceTimer = 0.0f;
            ActiveCars = new List<GameObject>();
        }

        private void Update()
        {
            raceTimer += Time.deltaTime;
        }

        public void AddActiveCar ( GameObject Car )
        {
            ActiveCars.Add(Car);
        }
        public void CarFinished(GameObject Car, float timer)
        {
            Car.GetComponent<CarData>().finished = true;
            Car.GetComponent<CarData>().controlable = false;
            return;
        }
    }
}
