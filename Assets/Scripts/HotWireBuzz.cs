using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotWireBuzz : MonoBehaviour
{
    //Dieses Skript löst den Buzzer-Sound aus, wenn der Heiße Draht berührt wird
    //Es wurde separat erstellt, weil nur der Heiße Draht dieses Skript verwendet und keiner der anderen Tests
    [SerializeField] private AudioSource buzzer;

    
    //Wenn das Objekt mit diesem Skript als Komponnente eine Kollision mit einem Objekt eingeht, welches einen
    //Trigger-Collider hat und getaggt wurde mit Collision, dann wird der Buzzer-sound gespielt
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
    
    //Wenn der Trigger aus dem Objekt heraus geht, dann wird der Sound gestoppt
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
