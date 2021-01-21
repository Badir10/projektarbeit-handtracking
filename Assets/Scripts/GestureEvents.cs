using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureEvents : MonoBehaviour
{
    //Dies war ein Testskript mit dem ich zum Anfang schnell testen konnte wie gut das Handtracking reagiert
    //Es spawnt objekte und ändert die Farbe eines Kubus
    public List<GameObject> prefabs;
    public GameObject cube;
    public List<Color> colors;
    public List<SpriteRenderer> gestureSprites;


    private void Start()
    {
        if (GameObject.Find("GestenBilder_Tutorial(Clone)") != null && gestureSprites.Count == 0)
        {
            gestureSprites = GameObject.Find("GestenBilder_Tutorial(Clone)").GetComponent<GestureEvents>().gestureSprites;
        }
    }

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
        if (gestureSprites.Count > 0)
        {
            gestureSprites[index].color = Color.white;
        }
    }

    public void GestureNotRecognized(int index)
    {
        if (gestureSprites.Count > 0)
        {
            gestureSprites[index].color = Color.black;
        }
    }
}
