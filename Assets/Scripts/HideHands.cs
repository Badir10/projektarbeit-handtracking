using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHands : MonoBehaviour
{
    [SerializeField] private OVRHand handscript;

    [SerializeField] private GameObject lefthandAnchor;
    [SerializeField] private GameObject righthandAnchor;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (handscript.IsTracked)
        {
            lefthandAnchor.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            righthandAnchor.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(true);

            lefthandAnchor.transform.GetChild(4).gameObject.SetActive(false);
            righthandAnchor.transform.GetChild(4).gameObject.SetActive(false);
        }

        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0)
        {
            lefthandAnchor.transform.GetChild(4).gameObject.SetActive(true);
            righthandAnchor.transform.GetChild(4).gameObject.SetActive(true);
            
            lefthandAnchor.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            righthandAnchor.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
