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
    public List<GameObject> blocks;
    
    public GameObject box;

    public static bool boxcreated = false;

    void Start()
    {
        CreateBox();
    }
    
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
