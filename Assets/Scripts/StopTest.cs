using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTest : MonoBehaviour
{
    //dieses Skript sperrt die Tests, wenn der Timer nicht läuft, damit alle Tests gleich ablaufen
    //
    private Rigidbody objectRB;

    void Start()
    {
        objectRB = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Die timerrunning-Variable aus dem Countdown-Skript wird abgefragt und die Rigidbody-Komponnenten werden gesperrt,
        //wenn der Timer nicht läuft
        if (!Countdown.timerRunning) objectRB.constraints = RigidbodyConstraints.FreezeAll;

        if (Countdown.timerRunning) objectRB.constraints = RigidbodyConstraints.None;
    }
}
