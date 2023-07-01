using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LimitadordeFps : MonoBehaviour
{
    public int limiteDeFPS = 60;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
    }

    void Update()
    {
        if (limiteDeFPS != Application.targetFrameRate)
        {
            Application.targetFrameRate = limiteDeFPS;
        }
        
    }
}
