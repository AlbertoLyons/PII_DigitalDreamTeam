using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power_Pellet_UI : MonoBehaviour
{
    [SerializeField] private Image PowerPellet;
    
    public void PowerColor(bool powerStatus) {
        Debug.Log(powerStatus);
        if (powerStatus) {
            PowerPellet.color = new Color(0, 255, 9, 255);
            
        }   
        if (!powerStatus) {
            PowerPellet.color = new Color(255, 255, 255, 255);
        }

    }
}
