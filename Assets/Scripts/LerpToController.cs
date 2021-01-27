using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class LerpToController : MonoBehaviour
{
    
    //Erstes Skript um den Tisch zu projizieren. Wurde durch das Skript eines Komilitonen ersetzt (PositionController)
    //Dieses Skript diente zu Testzwecken
    public GameObject rightController;
    private GameObject table;

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            gameObject.transform.position = rightController.transform.position;
            gameObject.transform.position -= new Vector3(0, 0.07f, 0);
        }
    }
}
