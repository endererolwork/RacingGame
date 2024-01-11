using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class CheckPointOrderer : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            CheckPointData[] checkPointDatas = transform.GetComponentsInChildren<CheckPointData>();
            for (int i=0; i< checkPointDatas.Length; i++)
            {
                if (i == 0) 
                { 
                    checkPointDatas[i].firstCheckPoint = true;
                    checkPointDatas[i].nextCheckPointData = checkPointDatas[i+1]; 
                    checkPointDatas[i].previousCheckPointData = checkPointDatas[checkPointDatas.Length - 1]; 
                }
                else if (i == checkPointDatas.Length-1) 
                { 
                    checkPointDatas[i].nextCheckPointData = checkPointDatas[0]; 
                    checkPointDatas[i].previousCheckPointData = checkPointDatas[i-1]; }
                else
                {
                    checkPointDatas[i].nextCheckPointData = checkPointDatas[i + 1];
                    checkPointDatas[i].previousCheckPointData = checkPointDatas[i - 1];
                }
            }
        } 
    }
}
