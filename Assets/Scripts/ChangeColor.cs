using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Material handColor;
    // Start is called before the first frame update
    void Start()
    {
        handColor.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
