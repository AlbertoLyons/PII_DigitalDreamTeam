using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Multiplier_UI : MonoBehaviour
{   
    [SerializeField] private Image multiplier;
    [SerializeField] private Sprite multiplier2;
    [SerializeField] private Sprite multiplier3;
    [SerializeField] private AudioSource audiosource;
    [SerializeField] private AudioClip xCoins;

    public void showMultiplier(string num) {
        audiosource.clip = xCoins;
        audiosource.Play();

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