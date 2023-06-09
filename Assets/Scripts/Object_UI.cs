using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object_UI : MonoBehaviour
{
    [SerializeField] private Image shield;

    public void shieldColor(bool estadoEscudo) {

        if (estadoEscudo) {
            shield.color = new Color(0, 255, 9, 255);
            
        }   
        if (!estadoEscudo) {
            shield.color = new Color(255, 255, 255, 255);
        }

    }

}
