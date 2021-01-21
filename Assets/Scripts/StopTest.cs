using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTest : MonoBehaviour
{
    private Rigidbody objectRB;

    void Start()
    {
        objectRB = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        objectRB.constraints = RigidbodyConstraints.FreezeAll;
        
        if (Countdown.timerRunning) objectRB.constraints = RigidbodyConstraints.None;
    }
}
