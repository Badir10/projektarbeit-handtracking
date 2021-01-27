using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandMenuMethods : MonoBehaviour
{
    //In diesem Skript wurden alle Methoden für die Events der Menüs gespeichert
    //
    
    //hier werden alle Tests im Editor eingespeichert
    public List<GameObject> testPrefabs;
    private int prevIndex;
    private GameObject testInstance;

    //Die Position des Tests auf dem Tisch
    private Vector3 testPosition;
    private GameObject table;
    
    //Hier werden die Anweisungen für den Test gesammelt und mit der Text-Variable angezeigt
    [SerializeField] private string[] testAnweisungen;
    [SerializeField] private TMP_Text testAnweisungenDisplay;
    

    //Diese Methode switcht zwischen den Tests, wenn man das Menü nutzt
    public void DisplayTest(int index)
    {
        //Wenn ein Test angezeigt wird muss der Timer wieder auf 0 gestellt werden, damit dieser nicht im Hintergrund läuft
        Countdown.timerRunning = false;
        
        //Ursprüngliche Lösung - hier wurden die Tests nur aktiviert und deaktiviert
        //testPrefabs[prevIndex].gameObject.SetActive(false);
        //testPrefabs[index].gameObject.SetActive(true);

        
        //Hier hingegen werden die Tests jetzt instanziiert, wenn notwendig und bei Wechsel dann komplett aus der Szene gelöscht
        //Elegantere Lösung, so befinden sich nur die notwendigen Tests in der Szene und die alten werden gelöscht.
        
        //Zuerst wird der alte Test gelöscht, wenn die Testinstanz-Variable gefüllt ist
        if (testInstance != null)
        {
            Debug.Log(testInstance.gameObject.name);
            Destroy(testInstance);
        }
        
        //Wenn sich kein Tisch in der Szene befindet kann auch kein Test erstellt werden
        if (GameObject.Find("TableParent(Clone)") != null && GameObject.Find("TableParent(Clone)").activeInHierarchy)
        {
            //der Test an dem zugehörigen Index wird instanziiert und in testInstance gespeichert
            testInstance = Instantiate(testPrefabs[index]);
            
            //Nun werden die zugehörigen Anweisungen für 10 Sekunden angezeigt
            testAnweisungenDisplay.gameObject.SetActive(true);
            testAnweisungenDisplay.text = testAnweisungen[index];
            Invoke("TestTutorialDisappear", 10f);
        }
        
        
    }

    //Diese Methode positioniert den Test in der Mitte des neu erstellten Tisches
    public void TestPositioning(int index)
    {
        //Erst muss sichergestellt werden, dass ein Tisch in der Szene ist
        if (GameObject.Find("TableParent(Clone)") != null && GameObject.Find("TableParent(Clone)").activeInHierarchy)
        {
            //Nun wird der Rechte Eckpunkt des Tisches (PositionController-Skript wurde nicht von mir erstellt) in einer lokalen Variable gespeichert
            Vector3 rightPos = PositionController.Instance.getRightPos();
            
            //Die gewünschte Position wird für den Tisch in einer Variable gespeichert, in diesem Fall die Mitte des Tisches
            testPosition = new Vector3(rightPos.x/2, transform.localPosition.y, rightPos.z + 0.1f);
            //Der Tisch wird in einer Variable gespeichert, damit GameObject.Find nicht öfter verwendet werden muss (Ist eine teure Methode)
            table = GameObject.Find("TableParent(Clone)");
        
            //Da sich auch der Timer oder Counter in dem testInstance GameObject befindet wird nur das erste Kind-Objekt genommen und an die gewünschte Position platziert
            testInstance.gameObject.transform.GetChild(0).gameObject.transform.position = new Vector3(table.transform.localPosition.x, table.transform.localPosition.y + 0.05f, table.transform.localPosition.z);
            testInstance.gameObject.transform.GetChild(0).gameObject.transform.rotation = table.transform.localRotation;        
        }
    }
    
    //Diese Methode wurde erstellt um den Test zu starten
    public void StartTest()
    {
        if (!Countdown.timerRunning && testInstance != null && GameObject.Find(testInstance.name).activeInHierarchy)
        {
            Countdown.timerRunning = true;
        }
        else if (Countdown.timerRunning && testInstance.name == "StackObjects(Clone)")
        {
            Countdown.timerRunning = false;
        }
    }

    //Diese Methode wurde nur erstellt, weil man so eine Zeitverzögerung einbauen konnte über Invoke s. Zeile 57
    public void TestTutorialDisappear()
    {
        testAnweisungenDisplay.gameObject.SetActive(false);
    }
}
