using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{

    public static bool timerRunning = true;
    public float remainingTime = 15;
    
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
        textDisplay = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        Timer();
        DisplayTime(remainingTime);
        
    }

    public void Timer() // in Klammern später Variable für die Zeit eingeben, wenn es mit Button verbunden werden soll
    {
        if (timerRunning)
        {
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

    public void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        textDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
