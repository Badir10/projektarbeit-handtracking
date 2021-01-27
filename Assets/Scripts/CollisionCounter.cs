using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionCounter : MonoBehaviour
{
    //Durch dieses Skript werden die Kollisionen mit Trigger-Collidern gemessen, welche an Objekten befestigt sind
    //die den Tag "Collision" haben
    //Dies wird genutzt um beim Heißen Draht die Berührungen zu zählen und um beim Box and Blocks test die erfolgreich
    //platzierten Boxen zu zeigen
    //
    
    //Anzahl der Kollisionen
    public int collisionCount = 0;
    public Text countDisplay;
    //Wurde für den fall erstellt, dass eine falsche Kollision erfasst wird
    private bool wrongCollision;

    //wurde für den gescheiterten Versuch verwendet den Griff zu lösen, wenn man mit der Trennwand in der Mitte
    //des Box and Blocks Test kollidiert
    [SerializeField] private OVRGrabber grab;
    public OVRGrabber[] grabbers;

    private void Start()
    {
        //Speichert alle Grabber und somit auch alle Hände, welche sich in der Szene befinden
        grabbers = FindObjectsOfType<OVRGrabber>();
    }

    private void Update()
    {
        //Updatet den Counter, mit der aktuellen Anzahl an Kollisionen
        countDisplay.text = collisionCount + "";
    }

    //Wenn eine Kollision entsteht wird der Collision count um 1 erhöht
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collision") && gameObject.name != "Trennwand")
        {
            if (Countdown.timerRunning)
            {
                collisionCount++; 
            }
        }
    }

    //Ein gescheiterter Versuch die gehaltenen Boxen loszulassen, wenn sie gegen die Trennwand kommen
    //Dieser Code scheiterte an der Methode ForceRelease vom Oculus Grabber, die kollision wurde richtig erkannt,
    //aber die Methode wurde nicht abgespielt
    
    /*private void OnCollisionEnter(Collision other)
    {
        if (gameObject.name == "Trennwand" && other.gameObject.CompareTag("Collision"))
        {
            foreach (OVRGrabber grabber in grabbers)
            {
                if (grabber.grabbedObject != null && grabber.grabbedObject.gameObject.name == other.gameObject.name)
                {
                    //Alles funktioniert, bis auf das ForceRelease, es soll eigentlich ein Objekt loslassen,
                    //wenn es gegrabbed wird, aber das macht es einfach nicht.
                    Debug.Log("THAAAAAATS SOOOO ILLLLEEEGAAAAAl");
                    grabber.ForceRelease(other.gameObject.GetComponent<OVRGrabbable>());

                }
            }
        }
    }*/

    // Wenn die Boxen aus der rechten Seite wieder rausgenommen werden wird der Collision Count aktualisiert,
    // somit zählt der Counter effektiv nur die Boxen, welche sich in der rechten Seite befinden und nicht die, welche herausgefallen sind
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
