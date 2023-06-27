using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shield_UI : MonoBehaviour
{
    [SerializeField] private Image shield;
    [SerializeField] private TMP_Text contador;
    [SerializeField] private AudioSource audiosource;
    [SerializeField] private AudioClip shieldSound;
    
    void Start()
    {
        shield.color = new Color(255, 255, 255, 0);
        contador.color = new Color(255, 255, 255, 0);
    }

    void Update()
    {
        contador.text = Player.countShield.ToString();
    }

    public void shieldColor(bool estadoEscudo) {
        if (estadoEscudo) {
            shield.color = new Color(255, 255, 255, 255);
            contador.color = new Color(255, 255, 255, 255);
            contador.text = Player.countShield.ToString();
            audiosource.clip = shieldSound;
            audiosource.Play();
            
        }   
        if (!estadoEscudo) {
            shield.color = new Color(255, 255, 255, 0);
            contador.color = new Color(255, 255, 255, 0);
            audiosource.clip = shieldSound;
            audiosource.Play();
        }

    }
}
