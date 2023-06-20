using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Meteorito : MonoBehaviour
{
    [SerializeField] private float rotacion = 1.0f;
    [SerializeField] private float velocidad = 0.08f;
    [SerializeField] private AudioSource audiosource;
    [SerializeField] private GameObject menuMuerte;
    AudioMenu menu;
   
    private void Start()
    {
        rotacion = 1.0f;
        velocidad = 0.08f;
        Time.timeScale = 1f;
    }
  
    void Update()
    {

         transform.Rotate(0,0,rotacion,Space.Self);
         transform.Translate(0,-velocidad,0,Space.World);
         

    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(other.gameObject);
            audiosource.Play();
            menu = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioMenu>();
            menu.stopMusic();
            StartCoroutine(Coroutine());
        }
    }
    public void ChangeScene(string nameScene) {
        SceneManager.LoadScene(nameScene);
    }
    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(2);
        menuMuerte.SetActive(true);
        Time.timeScale = 0f;
        velocidad = 0f;
        rotacion = 0f;
        //ChangeScene("Menu");
        
    }
}
