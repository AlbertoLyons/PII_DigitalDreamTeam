using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object_UI : MonoBehaviour
{
    [SerializeField] private Image shield;
    [SerializeField] private Image multiplier;
    [SerializeField] private Sprite multiplier2;
    [SerializeField] private Sprite multiplier3;

 
    public void shieldColor(bool estadoEscudo) {

        if (estadoEscudo) {
            shield.color = new Color(0, 255, 9, 255);
            
        }   
        if (!estadoEscudo) {
            shield.color = new Color(255, 255, 255, 255);
        }

    }
    public void showMultiplier(string num) {
        if (num == "X2") {
            multiplier.enabled = true;
            multiplier.sprite = multiplier2;
        }
        else if (num == "X3") {
            multiplier.enabled = true;
            multiplier.sprite = multiplier3;
        }
        
    }
    public void hideMultiplier() {
        multiplier.enabled = false; 
    }
}