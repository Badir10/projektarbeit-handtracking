using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionCounter : MonoBehaviour
{
    public int collisionCount = 0;
    public Text countDisplay;
    
    // Update is called once per frame
    void Update()
    {
        countDisplay.text = collisionCount + "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collision"))
        {
            if (Countdown.timerRunning)
            {
                collisionCount++;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Collision"))
        {
            if (Countdown.timerRunning && gameObject.name != "Pipe")
            {
                collisionCount--;
            }
        }
    }
}
