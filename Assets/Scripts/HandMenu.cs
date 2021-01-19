﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandMenu : MonoBehaviour
{
    public UnityEvent indexEvent;
    public UnityEvent middleEvent;
    public UnityEvent ringEvent;
    public UnityEvent pinkyEvent;
    
    public UnityEvent defaultEvent;

    private OVRHand hand;
    public OVRSkeleton skeleton;

    public GameObject[] handmenuItems;
    public List<Vector3> fingerTipPos;

    private bool menuisOpened = false;
    
    // Start is called before the first frame update
    void Start()
    {
        hand = gameObject.GetComponent<OVRHand>();
        skeleton = gameObject.GetComponent<OVRSkeleton>();
    }

    // Update is called once per frame
    void Update()
    {
        //Pinches speichern
        bool isIndexFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        bool isRingFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Ring);
        bool isMiddleFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Middle);
        bool isPinkyFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Pinky);

        
        // Menü öffnen, wenn Option gedrückt wird (5 Sekunden Zeigefinger Pinch) und das Menü nicht schon geöffnet ist
        if (OVRInput.GetDown(OVRInput.Button.Start) && !menuisOpened)
        {
            menuisOpened = true;
        }
        else if (OVRInput.GetDown(OVRInput.Button.Start) && menuisOpened)
        {
            menuisOpened = false;
        }
        
        
        //Menü ist geöffnet
        //Menüitems an die Positionen der Fingerspitzen platzieren
        //Events werden durchgegeben
        if (menuisOpened)
        {
            LerpMenuItems();
            
            //Events aktivieren bei Pinch - schönere Lösung finden!
            if (isIndexFingerPinching)
            {
                indexEvent.Invoke();
            }
            else
            {
                defaultEvent.Invoke();
            }
            
            if (isRingFingerPinching)
            {
                ringEvent.Invoke();
            }
            else
            {
                defaultEvent.Invoke();
            }
            
            if (isMiddleFingerPinching)
            {
                middleEvent.Invoke();
            }
            else
            {
                defaultEvent.Invoke();
            }
            
            if (isPinkyFingerPinching)
            {
                pinkyEvent.Invoke();
            }
            else
            {
                defaultEvent.Invoke();
            }
        }
        else
        {
            //wenn menü geschlossen ist
            foreach (GameObject go in handmenuItems)
            {
                go.SetActive(false);
            }
        }

    }

    void LerpMenuItems()
    {
        // Alle Menüitems anschalten
        foreach (GameObject go in handmenuItems)
        {
            go.SetActive(true);
        }
        
        foreach(OVRBone bone in skeleton.Bones) {
            if (bone.Id == OVRSkeleton.BoneId.Hand_IndexTip)
            {
                Vector3 bonepos = bone.Transform.position;
                fingerTipPos.RemoveAt(0);
                fingerTipPos.Insert(0, bonepos);
                handmenuItems[0].transform.position = fingerTipPos[0];
            }
            else if (bone.Id == OVRSkeleton.BoneId.Hand_MiddleTip)
            {
                Vector3 bonepos = bone.Transform.position;
                fingerTipPos.RemoveAt(1);
                fingerTipPos.Insert(1, bonepos);
                handmenuItems[1].transform.position = fingerTipPos[1];
            }
            else if (bone.Id == OVRSkeleton.BoneId.Hand_RingTip)
            {
                Vector3 bonepos = bone.Transform.position;
                fingerTipPos.RemoveAt(2);
                fingerTipPos.Insert(2, bonepos);
                handmenuItems[2].transform.position = fingerTipPos[2];
            }
            else if (bone.Id == OVRSkeleton.BoneId.Hand_PinkyTip)
            {
                Vector3 bonepos = bone.Transform.position;
                fingerTipPos.RemoveAt(3);
                fingerTipPos.Insert(3, bonepos);
                handmenuItems[3].transform.position = fingerTipPos[3];
            }
        }
    }
}
