using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureEvents : MonoBehaviour
{
    public List<GameObject> prefabs;
    public GameObject cube;
    public List<Color> colors;
    public List<SpriteRenderer> gestureSprites;

    public void Spawn(int index)
    {
        Instantiate(prefabs[index], transform.position, transform.rotation);
    }

    public void ChangeColor(int index)
    {
        cube.GetComponent<Renderer>().material.color = colors[index];
    }

    public void ChangeColorBack()
    {
        gestureSprites[0].color = Color.black;
        gestureSprites[1].color = Color.black;
        gestureSprites[2].color = Color.black;
        gestureSprites[3].color = Color.black;
        gestureSprites[4].color = Color.black;
        gestureSprites[5].color = Color.black;
        gestureSprites[6].color = Color.black;
    }
    
    public void GestureRecognized(int index)
    {
        //Invoke("ChangeColorBack", 1);
        gestureSprites[index].color = Color.white;
    }

    public void GestureNotRecognized(int index)
    {
        gestureSprites[index].color = Color.black;
    }
}
