using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionCounter : MonoBehaviour
{
    public int collisionCount = 0;
    private Text countDisplay;
    
    // Start is called before the first frame update
    void Start()
    {
        countDisplay = GameObject.Find("BoxCount").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        countDisplay.text = collisionCount + " Boxen";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collision"))
        {
            collisionCount++;
        }
    }
}
