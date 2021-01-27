using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class RadialMenu : MonoBehaviour
{
    //In diesem Skript wird das Radial-Menü gesteuert, welches für die Nutzung mit dem Controller erstellt wurde
    
    //Variable die den Zustand des Menüs speichert
    private bool menuisOpened;
    
    //Hier werden die Menüpunkte gespeichert
    [SerializeField] private List<GameObject> menuItems;
    [SerializeField] private List<SpriteRenderer> menuItemsRend;
    
    //Thumbstickidle prüft, ob der Thumbstick sich aktuell bewegt oder nicht
    private bool thumbstickIdle = false;
    
    //Ähnlich wie ein hover bei dem Mauszeiger, da der Joystick einen wert von -1 bis 1 speichert
    //in x- und y- Richtung ist ein float von 0,25 ein Zustand in dem der Joystick nur leicht nach vorne gedrückt ist
    [SerializeField] private float hover;
    //Ähnlich wie ein Mausiklick
    [SerializeField] private float click;
    
    //Alle Events die der Controller in den verschiedenen Zuständen auslösen soll
    public UnityEvent upEvent;
    public UnityEvent rightEvent;
    public UnityEvent downEvent;
    public UnityEvent leftEvent;
    public UnityEvent defaultEvent;

    //Ein Event für den Klick des A-Buttons
    [FormerlySerializedAs("xButtonEvent")] public UnityEvent aButtonEvent;
    void Update()
    {
        //Menü öffnet sich, wenn der Start-Button am linken Controller gedrückt wird und das menü vorher geschlossen war
        if (OVRInput.GetDown(OVRInput.Button.Start) && !menuisOpened)
        {
            //Dann schalltet der Boolean um und die Menuitems werden angezeigt
            menuisOpened = true;
            foreach (GameObject go in menuItems) go.SetActive(true);
        }
        //Menü schließt sich, wenn der Start-Button wieder gedrückt wird und das menü vorher geöffnet war
        else if (OVRInput.GetDown(OVRInput.Button.Start) && menuisOpened)
        {
            //Dann schaltet der Boolean um und die Menüitems werden ausgeschaltet
            //Falls eines der Icons eingefärbt wurde durch den Hover wird dies wieder zurückgesetzt
            menuisOpened = false;
            foreach (GameObject go in menuItems) go.SetActive(false);
            foreach (SpriteRenderer rend in menuItemsRend) rend.color = Color.black;
        }
        
        //Separate abfrage für die Events - verwendet den menuisopened-Bool um den Zustand des Menüs in erfahrung zu bringen
        if (menuisOpened)
        {
            //Up
            if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y > hover)
            {
                //Hier wurden die Schleifen und Bedingungen der Lesbarkeit-Halber abgekürzt, da das Skript sonst weitaus länger und unübersichtlicher geworden wäre
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
        //Event für drücken des A-Buttons
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            aButtonEvent.Invoke();
        }

        
        //Diese Methode löst code aus, welcher in allen Unityevents (Zeile: 65, 77, 98, 102) verwendet wird.
        //Also wurde dieser separat gespeichert (hier) und nutzt lediglich den Namen des zugehörigen Events um diese zu unterscheiden
        void RadialEvent(UnityEvent myevent)
        {
            menuisOpened = false;
            foreach (GameObject go in menuItems) go.SetActive(false);
            foreach (SpriteRenderer rend in menuItemsRend) rend.color = Color.black;
            myevent.Invoke();
        }
    }
}
