using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LimitadordeFps : MonoBehaviour
{
    private int limiteDeFPS = 60;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = limiteDeFPS;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
