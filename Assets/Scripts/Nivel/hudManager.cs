using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class hudManager : GenericSingleton<hudManager>
{
    public static bool isPausado;
    public GameObject menuPausa;
    public TextMeshProUGUI _textMoneyCount; //serializefield hace que el valor privado se vea en el unity
    public int _moneyCount;
    private float auxVelocidad;
    private float auxMeteorito;
    private float rbAux;
    private float auxMeteoritoVelocidad;
    private float auxTime;


    void Start()
    {
        isPausado = false;
        menuPausa.SetActive(false);
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if(isPausado)
            {
                continuarJuego();
            }
            else
            {
                pausaJuego();
            }

        }
    }
    
    public void pausaJuego()
    {
        AudioMenu menu = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioMenu>();
        menuPausa.SetActive(true);
        if (Time.timeScale == 0.5f) {
            auxTime = 0.5f;
            auxVelocidad = Player.velocidad;
            auxMeteoritoVelocidad = Meteorito.velocidad;
            rbAux = 3f;
        } else {
            auxTime = 1f;
            rbAux = 1.9f;
            auxVelocidad = 0.1f;
        }
        Time.timeScale = 0f;
        isPausado = true;
        if (Player.velocidad != 0.107f) {
            auxVelocidad = Player.velocidad;
        } else {auxVelocidad = 0.1f;}
        
        Meteorito meteorScript = GameObject.FindGameObjectWithTag("Meteorito").GetComponent<Meteorito>();
        meteorScript.enabled = false;
        Player.velocidad = 0f;
        Player playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerScript.enabled = false;
        //Animator playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        //playerAnimator.enabled = false;
        menu.pauseMusic();

        //var randomNumber = Random.Range(1,99);
        
    }

    public void continuarJuego()
        {
            AudioMenu menu = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioMenu>();
            menuPausa.SetActive(false);
            Time.timeScale = auxTime;
            isPausado = false;
            Player.velocidad = auxVelocidad;
            Player.rb.gravityScale = rbAux;
            Meteorito meteorScript = GameObject.FindGameObjectWithTag("Meteorito").GetComponent<Meteorito>();
            meteorScript.enabled = true;
            Player playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            playerScript.enabled = true;
            menu.playMusic();
        }
    public void AddMoney(int value)
    {

        _moneyCount = value;

        _textMoneyCount.text = "" + _moneyCount;

    }
}
