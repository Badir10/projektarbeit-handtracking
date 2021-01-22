using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandMenuMethods : MonoBehaviour
{
    public List<GameObject> testPrefabs;
    private int prevIndex;
    private GameObject testInstance;

    private Vector3 testPosition;
    private GameObject table;

    public static bool boxnbloxon;

    [SerializeField] private string[] testAnweisungen;
    [SerializeField] private TMP_Text testAnweisungenDisplay;
    

    public void DisplayTest(int index)
    {
        Countdown.timerRunning = false;
        //Ursprüngliche Lösung
        //testPrefabs[prevIndex].gameObject.SetActive(false);
        //testPrefabs[index].gameObject.SetActive(true);

        //Elegantere Lösung, so befinden sich nur die notwendigen Tests in der Szene und die alten werden gelöscht.
        if (testInstance != null)
        {
            Debug.Log(testInstance.gameObject.name);
            
            //wenn ich diesen Schritt nicht mache, dann kann es passieren, dass die Hand mit dem zu löschenden Objekt kollidiert
            //und dabei der Collider der Hand ebenfalls mitgelöscht wird (wegen des Grabbers)
            //testInstance.transform.position = new Vector3(100, 100, 100);
            Destroy(testInstance);
        }
        
        if (GameObject.Find("TableParent(Clone)") != null && GameObject.Find("TableParent(Clone)").activeInHierarchy)
        {
            testInstance = Instantiate(testPrefabs[index]);
            
            testAnweisungenDisplay.gameObject.SetActive(true);
            testAnweisungenDisplay.text = testAnweisungen[index];
            Invoke("TestTutorialDisappear", 10f);
        }
        
        
    }

    public void TestPositioning(int index)
    {
        if (GameObject.Find("TableParent(Clone)") != null && GameObject.Find("TableParent(Clone)").activeInHierarchy)
        {
            Vector3 rightPos = PositionController.Instance.getRightPos();
            testPosition = new Vector3(rightPos.x/2, transform.localPosition.y, rightPos.z + 0.1f);
            table = GameObject.Find("TableParent(Clone)");
        
            //testPrefabs[index].gameObject.transform.GetChild(0).gameObject.transform.position = new Vector3(table.transform.localPosition.x, table.transform.localPosition.y + 0.05f, table.transform.localPosition.z);
            testInstance.gameObject.transform.GetChild(0).gameObject.transform.position = new Vector3(table.transform.localPosition.x, table.transform.localPosition.y + 0.05f, table.transform.localPosition.z);

            //testPrefabs[index].gameObject.transform.GetChild(0).gameObject.transform.rotation = table.transform.localRotation;
            testInstance.gameObject.transform.GetChild(0).gameObject.transform.rotation = table.transform.localRotation;        
        }
    }
    
    public void StartTest()
    {
        if (!Countdown.timerRunning && testInstance != null && GameObject.Find(testInstance.name).activeInHierarchy)
        {
            Countdown.timerRunning = true;
        }

        if (Countdown.timerRunning && testInstance.name == "StackObjects(Clone)")
        {
            Countdown.timerRunning = false;
        }
    }

    public void TestTutorialDisappear()
    {
        testAnweisungenDisplay.gameObject.SetActive(false);
    }
}
