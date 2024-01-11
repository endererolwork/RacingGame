using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public float raceTimer { get; private set; }

        public List<GameObject> ActiveCars; // host is always 0,

        List<CarData> FinishResults; // host is always 0,


        //-- rules below --
        [SerializeField]
        public int maxTours = 3;

        [SerializeField]

        public float startCount = 3.0f;

        public bool gameEnd = false;

        public bool allKartsGo = false;



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



            if (Input.GetKeyDown(KeyCode.P))
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                GameObject[] spawns = GameObject.FindGameObjectsWithTag("Respawn");
                //GameManager.Instance.ActiveCars.Add(gameObject);

                for (int i=0; i<players.Length; i++)
                {
                    players[i].transform.SetPositionAndRotation(spawns[i].transform.position, spawns[i].transform.rotation);
                }

                //spawns[0].gameObject.SetActive(false);
                //tra = spawns[0].transform;
                //spawnsList.Remove(spawns[0]);
            }
            /*if (!GameManager.Instance.allKartsGo && tra != null)
            {
                transform.SetPositionAndRotation(tra.position, tra.rotation);
            }*/
        }

        public void AddActiveCar ( GameObject Car )
        {
            ActiveCars.Add(Car);
        }
        public void CarFinished(GameObject Car, float timer)
        {
            
            Car.GetComponent<CarData>().finished = true;
            Car.GetComponent<CarData>().controlable = false;
            gameEnd = true;
            return;
        }

        public void AllKartsGo()
        {
            allKartsGo = true;
        }
    }
}
