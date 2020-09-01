using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IsPinchingTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var hand = GetComponent<OVRHand>();
        bool isIndexFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Ring);
        
        if (isIndexFingerPinching)
        {
            Debug.Log("Ringfinger is Pinched");
        }
    }
}
