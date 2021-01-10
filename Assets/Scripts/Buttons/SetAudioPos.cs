using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAudioPos : MonoBehaviour
{
    void Start(){
        Vector3 leftPos = PositionController.Instance.getLeftPos();
        transform.localPosition = new Vector3(leftPos.x + 0.1f, transform.localPosition.y, leftPos.z + 0.2f);
        foreach(Transform child in transform){
            child.gameObject.SetActive(true);
        }
    }
}
