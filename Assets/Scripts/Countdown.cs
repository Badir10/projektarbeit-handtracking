using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    //Dieses Skript steuert den Timer oder den Countdown, welcher für alle Tests angezeigt wurde
    //
    
    //Timerrunning wird in anderen Skripts verwendet und soll bei allen statisch sein
    public static bool timerRunning = false;
    [SerializeField] private float remainingTime = 60;

    //Hiermit kann zwischen Stopuhr und Countdown gewechselt werden
    public bool countdown = true;
    [SerializeField] private float maxtime;
    private float currentTime;
    
    private Text textDisplay;

    //Erster Versuch mit Enumerator, aber mit Time ist eleganter, weil richtig angezeigt werden kann ohne große Probleme.
    /*IEnumerator CountingDown()
    {
        secondsPassed = true;
        yield return new WaitForSeconds(1);
        countdownTime -= 1;

        if (countdownTime < 10)
        {
            textDisplay.text = "00:0" + countdownTime;
        }
        else if (countdownTime < 60)
        {
            textDisplay.text = "00:" + countdownTime;
        }
        secondsPassed = false;
    }*/
    
    void Start()
    {
        //nimmt sich das Gameobject, an welchem dieses Skript gebunden wird und kann somit einfach auf andere Gameobjects angewendet werden
        textDisplay = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        //Wenn countdown true ist, wird von der remainingtime herunter gezählt (also ein Countdown)
        if (countdown)
        {
            Timer();
            DisplayTime(remainingTime);
        }
        else
        {
            //Wenn countdown nicht true ist, wird die Stopuhr verwendet, also von 0 Sekunden hochgezählt
            //bis die maxtime erreicht wird
            StopWatch();
            DisplayTime(currentTime);
        }
        
    }

    //Timer Methode ist für den Countdown, 
    public void Timer()
    {
        if (timerRunning)
        {
            //Wenn der Countdown noch nicht bei 0 angekommen ist wird die Zeit immer um eine Sekunde heruntergerechnet
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else
            {
                remainingTime = 0;
                timerRunning = false;
            }
        }
    }

    //Stellt die Zeit korrekt dar
    public void DisplayTime(float timeToDisplay)
    {
        //minuten und Sekunden werden korrekt abgespeichert und dargestellt
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        //Die lokalen variablen werden mithilfe eines string.Formats im gängigen Minuten/Sekunden Format dargestellt
        textDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //Stopuhr-Methode: Fragt ab ob die maximale Zeit erreicht wird und zählt solange die Zeit mit drauf
    public void StopWatch()
    {
        if (timerRunning)
        {
            if (currentTime <= maxtime)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                currentTime = maxtime;
                timerRunning = false;
            }
        }
    }
    
    
}
