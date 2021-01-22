using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotWireBuzz : MonoBehaviour
{
    [SerializeField] private AudioSource buzzer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collision"))
        {
            if (Countdown.timerRunning)
            {
                buzzer.Play();
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Collision"))
        {
            if (Countdown.timerRunning)
            {
                buzzer.Stop();
            }
        }
    }
}
