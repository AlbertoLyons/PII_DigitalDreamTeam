using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager_Tienda : MonoBehaviour
{
    private Ftienda ftienda;
    public static int coins;
    public TMP_Text coinUI;
    public Item_Tienda[] itemsTiendaSO;
    public Tienda[] panelesTienda;
    public GameObject[] panelesTiendaGO;
    public Button[] BotonCompra;
    public static string[] keyCompras = {"cantVelocidad", "cantEscudo", "cantStopwatch", "cantPower Pellet", "cantCarne x2", "cantCarne x3"};
    public AudioClip buyingSound;
    public AudioSource audiosource;

    public float tiempoEspera = 2f;
    private bool esperandoInputHESOYAM = false; 
    private bool esperandoInputSZCMAWO = false; 
    private bool hPresionada = false;
    private bool ePresionada = false;
    private bool sPresionada = false;
    private bool oPresionada = false;
    private bool yPresionada = false;
    private bool aPresionada = false;
    private bool mPresionada = false;

    private bool sPresionada1 = false;
    private bool zPresionada1 = false;
    private bool cPresionada1 = false;
    private bool mPresionada1 = false;
    private bool aPresionada1 = false;
    private bool wPresionada1 = false;
    private bool oPresionada1 = false;

    // Start is called before the first frame update
    void Start()
    {
        CargarCompras();
        AddCoins();
        for (int i = 0; i < itemsTiendaSO.Length; i++)
            panelesTiendaGO[i].SetActive(true);
        coinUI.text = coins.ToString();
        CargarItems();
        IsComprable();

        ftienda = Ftienda.Instance;

    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("shop_coins", coins);
        
        //HESOYAM
        if (!esperandoInputHESOYAM && Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(EsperarInputHESOYAM());
            hPresionada = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ePresionada = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            sPresionada = true;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            oPresionada = true;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            yPresionada = true;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            aPresionada = true;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            mPresionada = true;
        }


        //SZCMAWO
        if (!esperandoInputSZCMAWO && Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(EsperarInputSZCMAWO());
            sPresionada1 = true;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            zPresionada1 = true;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            cPresionada1 = true;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            mPresionada1 = true;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            aPresionada1 = true;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            wPresionada1 = true;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            oPresionada1 = true;
        }
    }

    private IEnumerator EsperarInputHESOYAM()
    {
        esperandoInputHESOYAM = true;
        yield return new WaitForSeconds(tiempoEspera);

        if (hPresionada && ePresionada && sPresionada && oPresionada && yPresionada && aPresionada && mPresionada)
        {
            MeatGenerator();
        }

        hPresionada = false;
        ePresionada = false;
        sPresionada = false;
        oPresionada = false;
        yPresionada = false;
        aPresionada = false;
        mPresionada = false;
        esperandoInputHESOYAM = false;
    }



    private IEnumerator EsperarInputSZCMAWO()
    {
        esperandoInputSZCMAWO = true;
        yield return new WaitForSeconds(tiempoEspera);

        if (sPresionada1 && zPresionada1 && cPresionada1 && mPresionada1 && aPresionada1 && wPresionada1 && oPresionada1)
        {
            ResetCompras();
        }

        sPresionada1 = false;
        zPresionada1 = false;
        cPresionada1 = false;
        mPresionada1 = false;
        aPresionada1 = false;
        wPresionada1 = false;
        oPresionada1 = false;
        esperandoInputSZCMAWO = false;
    }

    public void AddCoins()
    {
        coins = PlayerPrefs.GetInt("shop_coins");
        coinUI.text = coins.ToString();
        IsComprable();
        PlayerPrefs.SetInt("run_coins", 0);
    }

    public void IsComprable()
    {
        for (int i = 0; i < itemsTiendaSO.Length; i++)
        {
            //si tiene suficiente dinero
            if (coins >= itemsTiendaSO[i].precio)
            {
                BotonCompra[i].interactable = true;
            }
            //si no tiene suficiente dinero
            else if (coins <= itemsTiendaSO[i].precio)    
            {
                // BotonCompra[i].interactable = false;
            }
        }
    }

    public void ComprarItem(int numBoton)
    {
        if (coins >= itemsTiendaSO[numBoton].precio)
        {            
            if (keyCompras.Contains("cant" + itemsTiendaSO[numBoton].nombre_item) && PlayerPrefs.GetInt("cant" + itemsTiendaSO[numBoton].nombre_item) < 7)
            {
                audiosource.clip = buyingSound;
                audiosource.Play();
                //suma la cantidad de veces que se ha comprado la mejora
                PlayerPrefs.SetInt("cant" + itemsTiendaSO[numBoton].nombre_item, PlayerPrefs.GetInt("cant" + itemsTiendaSO[numBoton].nombre_item) + 1);
                Debug.Log("Compraste " + itemsTiendaSO[numBoton].nombre_item + ": " + (PlayerPrefs.GetInt("cant" + itemsTiendaSO[numBoton].nombre_item)));
                //agrega cuadrado amarillo
                panelesTienda[numBoton].cantidad[PlayerPrefs.GetInt("cant" + itemsTiendaSO[numBoton].nombre_item) - 1].SetActive(true);
                //incrementa el precio por un factor de 1.8
                itemsTiendaSO[numBoton].precio = Convert.ToInt32((100 + 50 * numBoton) * Math.Pow(1.8, PlayerPrefs.GetInt("cant" + itemsTiendaSO[numBoton].nombre_item)));
                //cambia el precio del panel
                panelesTienda[numBoton].precio.text = itemsTiendaSO[numBoton].precio.ToString();
                //descuenta el precio del contador
                coins = coins - Convert.ToInt32((100 + 50 * numBoton) * Math.Pow(1.8, PlayerPrefs.GetInt("cant" + itemsTiendaSO[numBoton].nombre_item) - 1));
                //cambia el contador de monedas
                coinUI.text = coins.ToString();
                IsComprable();  

                // si tiene todas las mejoras se bloquea              
                if(PlayerPrefs.GetInt("cant" + itemsTiendaSO[numBoton].nombre_item) >= 7)
                {   
                    panelesTienda[numBoton].precio.text = "MAX";
                }                    
            }
        }
    }
    public void CargarItems()
    {
        for (int i = 0; i < itemsTiendaSO.Length; i++)
        {
            panelesTienda[i].nombre_item.text = itemsTiendaSO[i].nombre_item;
            panelesTienda[i].precio.text = itemsTiendaSO[i].precio.ToString();
            if (PlayerPrefs.GetInt("cant" + itemsTiendaSO[i].nombre_item) == 7)
            {
                panelesTienda[i].precio.text = "MAX";
            }
        }
    }
    public void CargarCompras()
    {
        for (int i = 0; i < keyCompras.Length; i++)
        {
            if (PlayerPrefs.GetInt(keyCompras[i]) > 0)
            {
                // Debug.Log(keyCompras[i] + ": " + PlayerPrefs.GetInt(keyCompras[i]));
                PlayerPrefs.SetInt(keyCompras[i], PlayerPrefs.GetInt(keyCompras[i]));
            }else
            {
                // Debug.Log(keyCompras[i] + ": 0");
                PlayerPrefs.SetInt(keyCompras[i], 0);
            }            
            if (PlayerPrefs.GetInt(keyCompras[i]) <= 7)
            {                
                for (int j = 0; j < PlayerPrefs.GetInt(keyCompras[i]); j++)
                {                    
                    panelesTienda[i].cantidad[j].SetActive(true);
                }
            }
            if (PlayerPrefs.GetInt("cant" + itemsTiendaSO[i].nombre_item) < 7)
            {
                //incrementa el precio por un factor de 1.8 al cargar (abrir) la escena (tienda)
                itemsTiendaSO[i].precio = Convert.ToInt32((100 + 50 * i) * Math.Pow(1.8, PlayerPrefs.GetInt("cant" + itemsTiendaSO[i].nombre_item)));
                panelesTienda[i].precio.text = itemsTiendaSO[i].precio.ToString();
            }                       
        }
    }
    public void ResetCompras()
    {
        Debug.Log("Se han borrado todas tus compras.");
        for (int i = 0; i < keyCompras.Length; i++)
        {
            if (PlayerPrefs.GetInt(keyCompras[i]) <= 7)
            {
                for (int j = 0; j < PlayerPrefs.GetInt(keyCompras[i]); j++)
                {                  
                    panelesTienda[i].cantidad[j].SetActive(false);
                }
            }
            PlayerPrefs.DeleteKey(keyCompras[i]);
            itemsTiendaSO[i].precio = 100 + 50 * i;
            panelesTienda[i].precio.text = itemsTiendaSO[i].precio.ToString();
        }
        coins = coins * 0;
        coinUI.text = coins.ToString();
        IsComprable();
    }
    public void MeatGenerator()
    {

        coins++;
        coins = coins * 99;
        coinUI.text = coins.ToString();
        IsComprable();  
    }
}
