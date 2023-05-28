using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    [SerializeField] private AudioSource audiosource;


    public void stopMusic() {
        audiosource.Stop();


    }
}
