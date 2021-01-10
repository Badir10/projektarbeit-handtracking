using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLimitSpring : MonoBehaviour
{
    // Hier soll sichergestellt werden, dass der Button nicht ueber und unter einen gewisses Limit hinausgeht
    
    [SerializeField]
    private Transform tablePosition;
    
    private Vector3 buttonPosition;

    private float distanceUp;

    private float distanceDown;

    private void Awake()
    {
        ///// Abstand zwischen Button und Tisch wird berechnet - wird verwendet um die Bewegung nach Unten zu messen /////
        ////
        
        distanceDown = Vector3.Distance(tablePosition.transform.position, transform.position);
        
        // Position des Tisches - wird verwendet um die Bewegung nach oben zu messen
        distanceUp = tablePosition.position.y;
        
        buttonPosition = transform.position;
    }

    void Update()
    {

        // Wenn der Button ueber seine Anfangsposition hinausgeht und der Tisch gerade nicht feinjustiert wird ...
        if (Vector3.Distance(tablePosition.transform.position, transform.position) >= distanceDown && !PositionController.Instance.getTablePosBool())
        {
            // ... dann soll die Position des buttons wieder auf seine Anfangsposition zurueckgestellt werden
            transform.position = buttonPosition;
        }

        // Wenn der Button sich unter den Tisch bewegt und der Tisch gerade nicht feinjustiert wird ...
        if (transform.position.y <= distanceUp && !PositionController.Instance.getTablePosBool())
        {
            // ... dann soll der Button sich wieder an die Tischposition zuruecksetzen
            transform.position = new Vector3(transform.position.x, distanceUp, transform.position.z);
        }

        // Wenn der Tisch gerade feinjustiert wird ...
        if (transform.position.y != tablePosition.position.y && PositionController.Instance.getTablePosBool())
        {
            // ... sollen alle Positionen auf die neuen Werte aktualisiert werden
            transform.localPosition = new Vector3(transform.localPosition.x, tablePosition.localPosition.y, transform.localPosition.z);
            distanceDown = Vector3.Distance(tablePosition.transform.position, transform.position);
            distanceUp = tablePosition.position.y;
            buttonPosition = transform.position;
        }
    }
}
