using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionCounter : MonoBehaviour
{
    public int collisionCount = 0;
    public Text countDisplay;
    
    void Start()
    {
        //countDisplay = GameObject.Find("BoxCount").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        countDisplay.text = collisionCount + "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collision"))
        {
            collisionCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Collision"))
        {
            //Wieder hinzufügen!
            //collisionCount--;
        }
    }
}
