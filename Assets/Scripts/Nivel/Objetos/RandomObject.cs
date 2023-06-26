using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObject : MonoBehaviour
{
    public GameObject[] objectList;
    
    void Start()
    {
        int random = Random.Range(1, objectList.Length);
        objectList[random].SetActive(true);
    }

  
}
