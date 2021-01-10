using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSkyBoxPos : MonoBehaviour
{
    void Start(){
        Vector3 rightPos = PositionController.Instance.getRightPos();
        transform.localPosition = new Vector3(rightPos.x - 0.1f, transform.localPosition.y, rightPos.z + 0.2f);
        foreach(Transform child in transform){
            child.gameObject.SetActive(true);
        }
    }
}
