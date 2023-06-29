using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_UI : MonoBehaviour
{
    [SerializeField] private AudioSource audiosource;
    [SerializeField] private AudioClip coin;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void RecogeMoneda()
    {
        audiosource.clip = coin;
        audiosource.Play();      
    }
}
