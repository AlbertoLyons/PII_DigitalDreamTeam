using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class hudManager : MonoBehaviour
{
    public static bool isPausado;
    public GameObject menuPausa;
    [SerializeField] private TextMeshProUGUI _textMoneyCount; //serializefield hace que el valor privado se vea en el unity
    [SerializeField] private int _moneyCount;
    public float auxVelocidad;
    void Start()
    {
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
        Time.timeScale = 0f;
        isPausado = true;
        if (Player.velocidad == 0.05f) {
            auxVelocidad = 0.05f;
        } else {auxVelocidad = 0.1f;}
        Player.velocidad = 0f;
        Meteorito.rotacion = 0f;
        Meteorito.velocidad = 0f;
        Player playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerScript.enabled = false;
        //Animator playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        //playerAnimator.enabled = false;
        menu.pauseMusic();
        
    }

    public void continuarJuego()
        {
            AudioMenu menu = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioMenu>();
            menuPausa.SetActive(false);
            Time.timeScale = 1f;
            isPausado = false;
            Player.velocidad = auxVelocidad;
            Meteorito.rotacion = 1.0f;
            Meteorito.velocidad = 0.08f;
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
