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
        Player.velocidad = 0f;
        Meteorito.rotacion = 0f;
        Meteorito.velocidad = 0f;
        menu.pauseMusic();
        
    }

    public void continuarJuego()
        {
            AudioMenu menu = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioMenu>();
            menuPausa.SetActive(false);
            Time.timeScale = 1f;
            isPausado = false;
            Player.velocidad = 0.1f;
            Meteorito.rotacion = 1.0f;
            Meteorito.velocidad = 0.08f;
            menu.playMusic();
        }


    public void AddMoney(int value)
    {

        _moneyCount = value;

        _textMoneyCount.text = "" + _moneyCount;

    }
}
