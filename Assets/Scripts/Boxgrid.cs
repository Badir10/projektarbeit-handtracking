using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxgrid : MonoBehaviour
{
    [SerializeField] private int width = 8;
    [SerializeField] private int depth = 7;
    [SerializeField] private int height = 3;
    [SerializeField] private float distance = 0.02f;
    
    //Liste in der alle Gameobjects gespeichert werden. Wichtig um diese in anderen Skripts bearbeiten zu können
    //ohne eine Find methode verwenden zu müssen, welche bei 150 Objekten teuer wäre
    public List<GameObject> blocks;
    
    //Gameobject welches in der Grid erstellt werden soll
    public GameObject box;

    public static bool boxcreated = false;

    void Start()
    {
        CreateBox();
    }
    
    //Erstellt das oben eingespeicherte Gameobject in einem Raster von width x depth x height, die werte können
    //im Editor eingestellt werden für bessere Optimierung
    public void CreateBox()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                for (int y = 0; y < height; y++)
                {
                    GameObject block = Instantiate(box, Vector3.zero, box.transform.rotation);
                    blocks.Add(block);
                    block.transform.parent = transform;
                    block.transform.localPosition = new Vector3(x*distance, y*distance, z*distance);
                }
            }
        }
        boxcreated = true;
    }

}
