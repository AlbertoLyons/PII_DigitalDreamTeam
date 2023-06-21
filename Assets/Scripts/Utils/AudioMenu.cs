using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    [SerializeField] private AudioSource audiosource;


    public void stopMusic() {
        audiosource.Stop();
    }
    public void pauseMusic() {
        audiosource.Pause();
    }
    public void playMusic() {
        audiosource.Play();
    }
}
