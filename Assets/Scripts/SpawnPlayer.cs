using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class SpawnPlayer : MonoBehaviour
    {
        [SerializeField]
        GameObject playerPrefab;
        // Start is called before the first frame update
        void Start()
        {
            GameObject[] spawns = GameObject.FindGameObjectsWithTag("Respawn");
            for (int i=0; i<spawns.Length; i++)
            {
                if (spawns[i].transform.childCount == 0)
                {
                    GameManager.Instance.ActiveCars.Add(Instantiate(playerPrefab, spawns[i].transform, false));
                    break;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            // bos  yere instantiate
        }
    }
}
