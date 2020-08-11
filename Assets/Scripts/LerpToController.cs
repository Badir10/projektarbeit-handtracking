using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class LerpToController : MonoBehaviour
{
    public GameObject rightController;
    private GameObject table;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            gameObject.transform.position = rightController.transform.position;
            gameObject.transform.position -= new Vector3(0, 0.07f, 0);
        }
    }
}
