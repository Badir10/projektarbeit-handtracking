using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHands : MonoBehaviour
{
    //Dieses Skript dient dazu die Hände vom Controller zu verstecken, wenn Handtracking aktiviert wird und andersherum
    //Dies wurde ursprünglich automatisch übernommen, da ich allerdings custom Hands verwende fürs tracking musste
    //ich dies selbst schreiben
    [SerializeField] private OVRHand handscript;
    [SerializeField] private OVRHand handscript2;


    [SerializeField] private GameObject lefthandAnchor;
    [SerializeField] private GameObject righthandAnchor;
    
    void Update()
    {
        //checkt, ob handtracking aktuell verwendet wird und aktiviert dann die Handtracking-Hände und deaktiviert die Controllertracking-Hände
        if (handscript.IsTracked || handscript2.IsTracked)
        {
            lefthandAnchor.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            righthandAnchor.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(true);

            lefthandAnchor.transform.GetChild(4).gameObject.SetActive(false);
            righthandAnchor.transform.GetChild(4).gameObject.SetActive(false);
        }

        //checkt, ob Controllertracking aktuell verwendet wird indem der Grabber geprüft wird (löst sehr leicht aus, wenn man den Controller greift, daher optimal
        //aktiviert dann die Controllertracking-Hände und deaktiviert die Handtracking-Hände
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0 || OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0)
        {
            lefthandAnchor.transform.GetChild(4).gameObject.SetActive(true);
            righthandAnchor.transform.GetChild(4).gameObject.SetActive(true);
            
            lefthandAnchor.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            righthandAnchor.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
