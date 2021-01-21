using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuMethods : MonoBehaviour
{
    public List<GameObject> testPrefabs;
    private int prevIndex;

    private Vector3 testPosition;
    private GameObject table;

    public static bool boxnbloxon;
    

    public void DisplayTest(int index)
    {
        testPrefabs[prevIndex].gameObject.SetActive(false);
        testPrefabs[index].gameObject.SetActive(true);
        prevIndex = index;
    }

    public void TestPositioning(int index)
    {
        Vector3 rightPos = PositionController.Instance.getRightPos();
        testPosition = new Vector3(rightPos.x/2, transform.localPosition.y, rightPos.z + 0.1f);
        
        table = GameObject.Find("TableParent(Clone)");
            
        testPrefabs[index].gameObject.transform.GetChild(0).gameObject.transform.position = new Vector3(table.transform.localPosition.x, table.transform.localPosition.y + 0.05f, table.transform.localPosition.z);
        testPrefabs[index].gameObject.transform.GetChild(0).gameObject.transform.rotation = table.transform.localRotation;
    }
    
    public void StoppingTime()
    {
        if (Countdown.timerRunning)
        {
            Countdown.timerRunning = false;
        }
        
    }

    public void StartTest()
    {
        if (!Countdown.timerRunning && testPrefabs[1].gameObject.activeInHierarchy)
        {
            Countdown.timerRunning = true;
        }
        if (Countdown.timerRunning)
        {
            Countdown.timerRunning = false;
        }
    }
}
