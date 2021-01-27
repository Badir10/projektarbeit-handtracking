using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTest : MonoBehaviour
{
    //dieses Skript sperrt die Tests, wenn der Timer nicht läuft.

    private Rigidbody objectRB;

    void Start()
    {
        objectRB = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!Countdown.timerRunning) objectRB.constraints = RigidbodyConstraints.FreezeAll;

        if (Countdown.timerRunning) objectRB.constraints = RigidbodyConstraints.None;
    }
}
