using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class HandGrab : OVRGrabber
{
    ///// diese Klasse erbt von dem Oculus OVRGrabber und aendert ihn so, dass er mit Handtracking funktionieren kann /////
    /// 
    
    private OVRHand hand;

    public SphereCollider fingertipGrabVol;
    private OVRSkeleton skeleton;
    
    [SerializeField] private float pinchThreshold = 0.7f;
    [SerializeField] private float grabVolRadius = 0.02f;
    

    protected override void Start()
    {
        // Fuehrt die Start-Methode des OVRGrabber aus und nimmt sich zusaetzlich das Handtracking-Prefab
        base.Start();
        hand = GetComponent<OVRHand>();
        //
        
        // Sucht die Position der Daumenspitze und platziert an dieser Stelle einen Collider, der als GrabVolume dient
        skeleton = GetComponent<OVRSkeleton>();
        fingertipGrabVol = GetComponent<SphereCollider>();
        /*foreach(OVRBone bone in skeleton.Bones) {
            if (bone.Id == OVRSkeleton.BoneId.Hand_ThumbTip) {
                fingertipGrabVol.center = bone.Transform.localPosition;
                //fingertipGrabVol.radius = grabVolRadius;

                //bone.Transform.gameObject.AddComponent<SphereCollider>();
                
            }
        }
        //hand.GetComponent<HandGrab>().m_grabVolumes[0] = fingertipGrabVol;
        */
    }

    public override void Update()
    {
        // Fuehrt die Update-Methode des OVRGrabber  und zusaetzlich die neue Grab-Methode fuer das Handtracking aus
        base.Update();
        CheckIndexPinch();
        
        foreach(OVRBone bone in skeleton.Bones) {
            if (bone.Id == OVRSkeleton.BoneId.Hand_ThumbTip) {
                //fingertipGrabVol.center = bone.Transform.localPosition;
            }
        }
        fingertipGrabVol.center = gameObject.transform.Find("Bones").transform.GetChild(0).transform.Find("Hand_Index1").transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.transform.position;
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
