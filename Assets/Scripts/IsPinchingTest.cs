using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IsPinchingTest : MonoBehaviour
{
    //Dieses Skript diente dazu schnell die Pinching-Geste testen zu können
    
    //Gameobjects als Indikator für die Gesten (Sind die Punkte in der Hand an der Wand)
    [SerializeField] private GameObject index;
    [SerializeField] private GameObject middle;
    [SerializeField] private GameObject ring;
    [SerializeField] private GameObject pinky;

    //Nimmt sich das OVRHand Skript von der auslösenden Hand
    [SerializeField] private OVRHand hand;

    private void Start()
    {
        //Speichert das OVRHand Skript aus der linken Hand in einer Variable
        hand = GameObject.Find("OVRCustomHandPrefab_L").GetComponent<OVRHand>();
    }

    void Update()
    {
        //Lokale Variablen speichern die verschiedenen Pinchgesten
        bool isRingFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Ring);
        bool isIndexFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        bool isPinkyFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Pinky);
        bool isMiddleFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Middle);
        
        //Wenn die jeweilige Geste erkannt wird soll der Indikator grün eingefärbt werden, wenn die Geste nicht gefunden
        //wird soll der Indikator grau bleiben
        if (isRingFingerPinching)
        {
            ring.GetComponent<Renderer>().material.color = Color.green;
            Debug.Log("Ringfinger is Pinched");
        }
        else
        {
            ring.GetComponent<Renderer>().material.color = Color.white;
        }
        
        if (isIndexFingerPinching)
        {
            index.GetComponent<Renderer>().material.color = Color.green;
            Debug.Log("Indexfinger is Pinched");
        }
        else
        {
            index.GetComponent<Renderer>().material.color = Color.white;
        }
        
        if (isPinkyFingerPinching)
        {
            pinky.GetComponent<Renderer>().material.color = Color.green;
            Debug.Log("Pinkyfinger is Pinched");
        } 
        else
        {
            pinky.GetComponent<Renderer>().material.color = Color.white;
        }
        
        if (isMiddleFingerPinching)
        {
            middle.GetComponent<Renderer>().material.color = Color.green;
            Debug.Log("Middlefinger is Pinched");
        } 
        else
        {
            middle.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
