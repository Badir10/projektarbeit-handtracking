using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureEvents : MonoBehaviour
{
    public List<GameObject> prefabs;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(int index)
    {
        Instantiate(prefabs[index], transform.position, transform.rotation);
    }
}
