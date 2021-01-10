using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;

public class ButtonListener : MonoBehaviour
{
    ///// Gibt die events vor die bei Beruehrung der Collider verwendet werden sollen /////
    /// diese Events koennen im Unityeditor mit verschiedenen Eventhandler-Methoden bespielt werden
    /// 
    
    public UnityEvent proximityEvent;
    public UnityEvent defaultEvent;

    void Start()
    {
        GetComponent<ButtonController>().InteractableStateChanged.AddListener(InitiateEvent);
    }

    void InitiateEvent(InteractableStateArgs state)
    {
        // Proximityevent wird bei Beruehrung ausgeloest
        if (state.NewInteractableState == InteractableState.ProximityState)
        {
            proximityEvent.Invoke();
        }

        // Defaultevent, wenn man aus dem Proximityevent rauskommt
        else 
        {
            defaultEvent.Invoke();
        }
    }
}
