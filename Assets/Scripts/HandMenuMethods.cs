using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuMethods : MonoBehaviour
{
    public List<GameObject> testPrefabs;
    private int prevIndex;
    void Start()
    {
        
    }
    
    public void DisplayTest(int index)
    {
        testPrefabs[prevIndex].gameObject.SetActive(false);
        testPrefabs[index].gameObject.SetActive(true);
        prevIndex = index;
    }
}
