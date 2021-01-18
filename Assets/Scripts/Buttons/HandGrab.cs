using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class HandGrab : OVRGrabber
{
    ///// diese Klasse erbt von dem Oculus OVRGrabber und aendert ihn so, dass er mit Handtracking funktionieren kann /////
    /// 
    
    private OVRHand hand;

    private SphereCollider fingertipGrabVol;
    private OVRSkeleton skeleton;
    private Vector3 indexTipPos;

    [SerializeField] private GameObject grabSphere;
    
    [SerializeField] private float pinchThreshold = 0.7f;
    [SerializeField] private float grabVolRadius = 0.02f;
    

    protected override void Start()
    {
        // Fuehrt die Start-Methode des OVRGrabber aus und nimmt sich zusaetzlich das Handtracking-Prefab
        base.Start();
        hand = gameObject.GetComponent<OVRHand>();
        //
        
        // Sucht die Position der Daumenspitze und platziert an dieser Stelle einen Collider, der als GrabVolume dient
        skeleton = gameObject.GetComponent<OVRSkeleton>();
    }

    public override void Update()
    {
        // Fuehrt die Update-Methode des OVRGrabber  und zusaetzlich die neue Grab-Methode fuer das Handtracking aus
        base.Update();
        CheckIndexPinch();
        
        // Verfeinerung des Grabbers, indem Collider genau an Fingerspitze platziert wird und von da aus gegrabbed wird.
        // Vorher Grabber zu groß und Dinge wurden an falsche Position platziert beim grabben.
        
        // Geht alle "Knochen"-Punkte und Positionen durch und nimmt sich die Position der Zeigefingerspitze
        foreach(OVRBone bone in skeleton.Bones) {
            if (bone.Id == OVRSkeleton.BoneId.Hand_IndexTip) {
                indexTipPos = bone.Transform.position;
                grabSphere.transform.position = indexTipPos;
            }
        }
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
