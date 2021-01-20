using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHands : MonoBehaviour
{
    [SerializeField] private OVRHand handscript;

    [SerializeField] private GameObject lefthandAnchor;
    [SerializeField] private GameObject righthandAnchor;
    
    void Update()
    {
        //checkt, ob handtracking aktuell verwendet wird und aktiviert dann die Handtracking-Hände und deaktiviert die Controllertracking-Hände
        if (handscript.IsTracked)
        {
            lefthandAnchor.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            righthandAnchor.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(true);

            lefthandAnchor.transform.GetChild(4).gameObject.SetActive(false);
            righthandAnchor.transform.GetChild(4).gameObject.SetActive(false);
        }

        //checkt, ob Controllertracking aktuell verwendet wird indem der Grabber geprüft wird (löst sehr leicht aus, wenn man den Controller greift, daher optimal
        //aktiviert dann die Controllertracking-Hände und deaktiviert die Handtracking-Hände
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0)
        {
            lefthandAnchor.transform.GetChild(4).gameObject.SetActive(true);
            righthandAnchor.transform.GetChild(4).gameObject.SetActive(true);
            
            lefthandAnchor.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            righthandAnchor.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
