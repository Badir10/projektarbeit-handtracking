using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHands : MonoBehaviour
{
    [SerializeField] private OVRHand handscript;

    [SerializeField] private GameObject trackedHandL;
    [SerializeField] private GameObject trackedHandR;
    [SerializeField] private GameObject handL;
    [SerializeField] private GameObject handR;

    [SerializeField] private GameObject lefthandAnchor;
    [SerializeField] private GameObject righthandAnchor;

    public bool istracked;
    
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
            
            istracked = true;
        }

        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0)
        {
            Debug.Log("MAMASITA");
        }
        {
            lefthandAnchor.transform.GetChild(4).gameObject.SetActive(true);
            righthandAnchor.transform.GetChild(4).gameObject.SetActive(true);
            
            lefthandAnchor.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            righthandAnchor.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            
            istracked = false;
        }
    }
}
