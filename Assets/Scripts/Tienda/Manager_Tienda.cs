using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager_Tienda : MonoBehaviour
{

    public static int coins;
    public TMP_Text coinUI;
    public Item_Tienda[] itemsTiendaSO;
    public Tienda[] panelesTienda;
    public GameObject[] panelesTiendaGO;
    public Button[] BotonCompra;
    public static string[] keyCompras = {"cantVelocidad", "cantEscudo", "cantStopwatch", "cantPower Pellet", "cantCarne x2", "cantCarne x3"};

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
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("shop_coins", coins);
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
                BotonCompra[i].interactable = false;
            }
        }
    }

    public void ComprarItem(int numBoton)
    {
        if (coins >= itemsTiendaSO[numBoton].precio)
        {            
            if (keyCompras.Contains("cant" + itemsTiendaSO[numBoton].nombre_item) && PlayerPrefs.GetInt("cant" + itemsTiendaSO[numBoton].nombre_item) < 7)
            {
                //suma la cantidad de veces que se ha comprado la mejora
                PlayerPrefs.SetInt("cant" + itemsTiendaSO[numBoton].nombre_item, PlayerPrefs.GetInt("cant" + itemsTiendaSO[numBoton].nombre_item) + 1);
                Debug.Log("Compraste " + itemsTiendaSO[numBoton].nombre_item + ": " + (PlayerPrefs.GetInt("cant" + itemsTiendaSO[numBoton].nombre_item)));
                //agrega cuadrado amarillo
                panelesTienda[numBoton].cantidad[PlayerPrefs.GetInt("cant" + itemsTiendaSO[numBoton].nombre_item) - 1].SetActive(true);
                //incrementa el precio por un factor de 2.2
                itemsTiendaSO[numBoton].precio = Convert.ToInt32((100 + 50 * numBoton) * Math.Pow(2.2, PlayerPrefs.GetInt("cant" + itemsTiendaSO[numBoton].nombre_item)));
                //cambia el precio del panel
                panelesTienda[numBoton].precio.text = itemsTiendaSO[numBoton].precio.ToString();
                //descuenta el precio del contador
                coins = coins - Convert.ToInt32((100 + 50 * numBoton) * Math.Pow(2.2, PlayerPrefs.GetInt("cant" + itemsTiendaSO[numBoton].nombre_item) - 1));
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
                //incrementa el precio por un factor de 2.2 al cargar (abrir) la escena (tienda)
                itemsTiendaSO[i].precio = Convert.ToInt32((100 + 50 * i) * Math.Pow(2.2, PlayerPrefs.GetInt("cant" + itemsTiendaSO[i].nombre_item)));
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
