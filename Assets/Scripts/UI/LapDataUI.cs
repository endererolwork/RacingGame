using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Race
{
    public class LapDataUI : MonoBehaviour
    {
        TMPro.TextMeshProUGUI TimersUI;

        [SerializeField] bool self;
        // Start is called before the first frame update
        void Start()
        {
            TimersUI = GetComponent<TMPro.TextMeshProUGUI>();
            TimersUI.text = "";
        }

        // Update is called once per frame
        void Update()
        {
            TimersUI.text = "";
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < players.Length; i++)
            {
                CarController CC = players[i].GetComponent<CarController>();
                CarData data = players[i].GetComponent<CarData>();
                if (self && CC.IsLocalPlayer)
                {
                    TimersUI.text += "Your\n";
                    for (int j=0; j< data.lapTimes.Count; j++)
                    {
                        TimersUI.text += "Lap " + (j+1).ToString() + ": " + data.lapTimes[j].ToString() + "\n";
                    }
                }
                if (!self && !CC.IsLocalPlayer)
                {
                    TimersUI.text += "others'\n";
                    for (int j = 0; j < data.lapTimes.Count; j++)
                    {
                        TimersUI.text += "Lap " + (j + 1).ToString() + ": " + data.lapTimes[j].ToString() + "\n";
                    }
                }
            }
        }
    }
}
