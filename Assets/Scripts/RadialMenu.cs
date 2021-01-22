using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RadialMenu : MonoBehaviour
{
    private bool menuisOpened;
    [SerializeField] private List<GameObject> menuItems;
    [SerializeField] private List<SpriteRenderer> menuItemsRend;
    
    private bool thumbstickIdle = false;
    [SerializeField] private float hover;
    [SerializeField] private float click;
    
    public UnityEvent upEvent;
    public UnityEvent rightEvent;
    public UnityEvent downEvent;
    public UnityEvent leftEvent;
    public UnityEvent defaultEvent;

    public UnityEvent xButtonEvent;
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start) && !menuisOpened)
        {
            menuisOpened = true;
            foreach (GameObject go in menuItems) go.SetActive(true);
        }
        else if (OVRInput.GetDown(OVRInput.Button.Start) && menuisOpened)
        {
            menuisOpened = false;
            foreach (GameObject go in menuItems) go.SetActive(false);
            foreach (SpriteRenderer rend in menuItemsRend) rend.color = Color.black;
        }
        
        if (menuisOpened)
        {
            //Up
            if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y > hover)
            {
                foreach (SpriteRenderer rend in menuItemsRend) rend.color = Color.black;
                menuItemsRend[0].color = Color.white;
                if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y > click) RadialEvent(upEvent);
                if (OVRInput.GetDown(OVRInput.Button.Start) && menuisOpened)
                {
                    menuisOpened = false;
                    upEvent.Invoke();
                }
            }
            //Down
            else if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y < -hover)
            {
                foreach (SpriteRenderer rend in menuItemsRend) rend.color = Color.black;
                menuItemsRend[2].color = Color.white;
                if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y < -click) RadialEvent(downEvent);
                if (OVRInput.GetDown(OVRInput.Button.Start) && menuisOpened)
                {
                    menuisOpened = false;
                    downEvent.Invoke();
                }
            }
            //Right
            else if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x > hover)
            {
                foreach (SpriteRenderer rend in menuItemsRend) rend.color = Color.black;
                menuItemsRend[1].color = Color.white;
                if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x > click) RadialEvent(rightEvent);
                if (OVRInput.GetDown(OVRInput.Button.Start) && menuisOpened)
                {
                    menuisOpened = false;
                    rightEvent.Invoke();
                }
            }
            //Left
            else if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x < -hover)
            {
                foreach (SpriteRenderer rend in menuItemsRend) rend.color = Color.black;
                menuItemsRend[3].color = Color.white;
                if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x < -click) RadialEvent(leftEvent);
                if (OVRInput.GetDown(OVRInput.Button.Start) && menuisOpened)
                {
                    menuisOpened = false;
                    leftEvent.Invoke();
                }
            }
            //Default (Thumbstick in middle)
            else if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y < 0.05f || OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y > -0.05f || 
                     OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x < 0.05f || OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x > -0.05f)
            {
                defaultEvent.Invoke();
            }
        }
        //Event für drücken des x-Buttons
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            xButtonEvent.Invoke();
        }

        
        
        void RadialEvent(UnityEvent myevent)
        {
            menuisOpened = false;
            foreach (GameObject go in menuItems) go.SetActive(false);
            foreach (SpriteRenderer rend in menuItemsRend) rend.color = Color.black;
            myevent.Invoke();
        }
    }
}
