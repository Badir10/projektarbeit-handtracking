using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionCounter : MonoBehaviour
{
    public int collisionCount = 0;
    public Text countDisplay;
    private bool wrongCollision;

    [SerializeField] private OVRGrabber grab;
    public OVRGrabber[] grabbers;

    private void Start()
    {
        grabbers = FindObjectsOfType<OVRGrabber>();
    }

    private void Update()
    {
        countDisplay.text = collisionCount + "";
    }

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
