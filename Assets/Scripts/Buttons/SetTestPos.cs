using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTestPos : MonoBehaviour
{
    private Vector3 testPosition;
    void Start(){
        //Vector3 leftPos = PositionController.Instance.getLeftPos();
        Vector3 rightPos = PositionController.Instance.getRightPos();
        testPosition = new Vector3(rightPos.x/2, transform.localPosition.y, rightPos.z + 0.1f);
        
        
        /*foreach(Transform child in transform){
            //child.gameObject.SetActive(true);
        }*/
    }
}
