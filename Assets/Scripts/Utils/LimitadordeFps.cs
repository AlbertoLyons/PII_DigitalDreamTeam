using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LimitadordeFps : MonoBehaviour
{
    public int limiteDeFPS = 60;

    void Start()
    {
        Application.targetFrameRate = limiteDeFPS;
        QualitySettings.vSyncCount = 1;
    }

    void Update()
    {
        
    }
}
