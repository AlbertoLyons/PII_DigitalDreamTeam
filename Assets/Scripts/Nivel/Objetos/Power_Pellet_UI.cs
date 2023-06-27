using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power_Pellet_UI : MonoBehaviour
{
    [SerializeField] private Image PowerPellet;
    [SerializeField] private AudioSource audiosource;
    [SerializeField] private AudioClip powerPellet;
    void Start()
    {
        PowerPellet.color = new Color(255, 255, 255, 0);
    }
    public void PowerColor(bool powerStatus) {
        // Debug.Log(powerStatus);
        if (powerStatus) {
            audiosource.clip = powerPellet;
            audiosource.Play();
            PowerPellet.color = new Color(255, 255, 255, 255);
            
        }   
        if (!powerStatus) {
            PowerPellet.color = new Color(255, 255, 255, 0);
        }

    }
}
