using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Race
{
    public class ResultUI : MonoBehaviour
    {
        TMPro.TextMeshProUGUI resultUI;

        // Start is called before the first frame update
        void Start()
        {
            resultUI = GetComponent<TMPro.TextMeshProUGUI>();
            resultUI.text = "";
        }

        // Update is called once per frame
        void Update()
        {
            resultUI.text = "";
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < players.Length; i++)
            {
                CarController CC = players[i].GetComponent<CarController>();
                CarData data = players[i].GetComponent<CarData>();
                if (data.finished) 
                {
                    if (CC.IsLocalPlayer)
                    {
                        resultUI.text = "YOU WIN";
                        resultUI.color = Color.green;
                    }
                    else
                    {
                        resultUI.text = "YOU LOSE";
                        resultUI.color = Color.red;
                    }
                }

            }
        }
    }
}

