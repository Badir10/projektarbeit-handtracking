using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class HandGrab : OVRGrabber
{
    ///// diese Klasse erbt von dem Oculus OVRGrabber und aendert ihn so, dass er mit Handtracking funktionieren kann /////
    /// 
    
    private OVRHand hand;
    
    [SerializeField]
    private float pinchThreshold = 0.7f;
    
    protected override void Start()
    {
        // Fuehrt die Start-Methode des OVRGrabber aus und nimmt sich zusaetzlich das Handtracking-Prefab
        base.Start();
        hand = GetComponent<OVRHand>();
    }

    public override void Update()
    {
        // Fuehrt die Update-Methode des OVRGrabber  und zusaetzlich die neue Grab-Methode fuer das Handtracking aus
        base.Update();
        CheckIndexPinch();
    }
    
    
    // prueft ob der Zeigefinger den Daumen beruehrt und grabt dann
    void CheckIndexPinch()
    {
        float pinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);

        if(!m_grabbedObj && pinchStrength > pinchThreshold && m_grabCandidates.Count > 0)
        {
            GrabBegin();
        }
        else if(m_grabbedObj && ! (pinchStrength > pinchThreshold))
        {
            GrabEnd();
        }
    }
}
