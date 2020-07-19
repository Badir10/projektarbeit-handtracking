using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureEvents : MonoBehaviour
{
    public List<GameObject> prefabs;
    public GameObject cube;
    public List<Color> colors;

    public void Spawn(int index)
    {
        Instantiate(prefabs[index], transform.position, transform.rotation);
    }

    public void ChangeColor(int index)
    {
        cube.GetComponent<Renderer>().material.color = colors[index];
    }
    
    
}
