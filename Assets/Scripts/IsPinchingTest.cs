using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IsPinchingTest : MonoBehaviour
{
    [SerializeField] private GameObject index;
    [SerializeField] private GameObject middle;
    [SerializeField] private GameObject ring;
    [SerializeField] private GameObject pinky;

    void Update()
    {
        var hand = GetComponent<OVRHand>();
        bool isRingFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Ring);
        bool isIndexFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        bool isPinkyFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Pinky);
        bool isMiddleFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Middle);
        
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
