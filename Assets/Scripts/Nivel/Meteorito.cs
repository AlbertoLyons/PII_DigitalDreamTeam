using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Meteorito : MonoBehaviour
{
    public static float rotacion = 1.0f;
    public static float velocidad = 0.06f;
    public AudioSource audiosource;
    public AudioClip musicaMuerte;
    public AudioClip sonidoMuerte;
    public GameObject menuMuerte;
    public GameObject particulas;
    private AudioMenu menu;
    public static float auxVelocidad = velocidad;
    public GameObject player;
    public GameObject generadorNivel;
    public Player playerScript;
   
    private void Start()
    {
        menu = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioMenu>();
        rotacion = 1.0f;
        auxVelocidad = 0.06f;
        Time.timeScale = 1f;
        velocidad = 0;
    }
  
    void Update()
    {
        if (player.transform.position.y < 0.11f && player.activeSelf) {
            velocidad = auxVelocidad;
            generadorNivel.SetActive(true);
        } else {
            generadorNivel.SetActive(false);
            Player.ScorePoints = 0;
            Player.ScorePoints_2 = 0;
            Player.ScorePoints_3 = 0;
            Player.ScorePoints_4 = 0;
            }
        particulas.transform.Translate(0,-velocidad,0,Space.World);
        transform.Rotate(0,0,rotacion,Space.Self);
        transform.Translate(0,-velocidad,0,Space.World);

    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            // muerte de jugador
            playerScript.enabled = false;
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            other.gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            other.gameObject.transform.Find("Particulas Correr").gameObject.SetActive(false);
            audiosource.clip = sonidoMuerte;
            audiosource.Play();
            //guarda monedas de la run al morir y volver a jugar
            PlayerPrefs.SetInt("run_coins", Player.coins);  
            PlayerPrefs.SetInt("shop_coins", Manager_Tienda.coins + Player.coins); 
            Manager_Tienda.coins = PlayerPrefs.GetInt("shop_coins");
            
            menu.stopMusic();
            //var HudManager = GameObject.FindGameObjectWithTag("HudManager");
            //HudManager.SetActive(false);
            StartCoroutine(Coroutine());
        }
    }
    public void ChangeScene(string nameScene) {
        SceneManager.LoadScene(nameScene);
    }
    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(2);
        audiosource.clip = musicaMuerte;
        player.SetActive(false);
        audiosource.Play();
        menuMuerte.SetActive(true);
        Time.timeScale = 0f;
        velocidad = 0f;
        rotacion = 0f;        
    }
}
