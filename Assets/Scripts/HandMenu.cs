using System.Collections;
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
    [SerializeField] private SkinnedMeshRenderer handMesh;


    public GameObject[] handmenuItems;
    public List<Vector3> fingerTipPos;

    private bool menuisOpened = false;

    private bool menuEntered = false;
    private bool prevMenuEntered;
    private bool pinching = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        hand = gameObject.GetComponent<OVRHand>();
        skeleton = gameObject.GetComponent<OVRCustomSkeleton>();
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
            handMesh.materials[0].color = Color.green;
            menuisOpened = true;
            pinching = true;
        }
        else if (OVRInput.GetDown(OVRInput.Button.Start) && menuisOpened)
        {
            menuisOpened = false;
            handMesh.materials[0].color = Color.white;
        }
        
        
        //Menü ist geöffnet
        //Menüitems an die Positionen der Fingerspitzen platzieren
        //Events werden durchgegeben
        if (menuisOpened)
        {
            if (gameObject.name == "OVRHandPrefab") LerpMenuItems();
            
            //Events aktivieren bei Pinch - Code wird hier kürzer dargestellt, da sonst zu lang
            if (isIndexFingerPinching)
            {
                if (!pinching)
                {
                    PinchEvent(indexEvent);
                }
            }
            else
            {
                defaultEvent.Invoke();
                pinching = false;
            }
            

            if (isRingFingerPinching) PinchEvent(ringEvent);
            else defaultEvent.Invoke();
            
            if (isMiddleFingerPinching) PinchEvent(middleEvent);
            else defaultEvent.Invoke();
            
            if (isPinkyFingerPinching) PinchEvent(pinkyEvent);
            else defaultEvent.Invoke();
            
        }
        else
        {
            //Dies soll auch nur passieren, wenn das normale OVRHandPrefab verwendet wird
            if (gameObject.name == "OVRHandPrefab")
            {
                //wenn menü geschlossen ist
                foreach (GameObject go in handmenuItems)
                {
                    go.SetActive(false);
                } 
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
    void PinchEvent(UnityEvent myevent)
    {
        menuisOpened = false;
        handMesh.materials[0].color = Color.white;
        myevent.Invoke();
    }
}
