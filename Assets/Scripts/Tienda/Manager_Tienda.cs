using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager_Tienda : MonoBehaviour
{

    public int coins;
    public TMP_Text coinUI;
    public Item_Tienda[] itemsTiendaSO;
    public Tienda[] panelesTienda;
    public GameObject[] panelesTiendaGO;
    public Button[] BotonCompra;
    private string[] keyCompras = {"cantVelocidad", "cantEscudo", "cantStopwatch", "cantPower Pellet", "cantCarne x2", "cantCarne x3"};
    // Start is called before the first frame update
    void Start()
    {
        CargarCompras();
        AddCoins();
        PlayerPrefs.SetInt("run_coins", 0);
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
        coins = PlayerPrefs.GetInt("shop_coins") + PlayerPrefs.GetInt("run_coins");
        coinUI.text = coins.ToString();
        IsComprable();
    }

    public void IsComprable()
    {
        for (int i = 0; i < itemsTiendaSO.Length; i++)
        {
            if (coins >= itemsTiendaSO[i].precio) //si tiene suficiente dinero
                BotonCompra[i].interactable = true;
            else
                BotonCompra[i].interactable = false;

        }
    }

    public void ComprarItem(int numBoton)
    {
        if (coins >= itemsTiendaSO[numBoton].precio)
        {
            
            if (keyCompras.Contains("cant" + itemsTiendaSO[numBoton].nombre_item) && PlayerPrefs.GetInt("cant" + itemsTiendaSO[numBoton].nombre_item) < 7)
            {
                PlayerPrefs.SetInt("cant" + itemsTiendaSO[numBoton].nombre_item, PlayerPrefs.GetInt("cant" + itemsTiendaSO[numBoton].nombre_item) + 1);
                Debug.Log("Compraste " + itemsTiendaSO[numBoton].nombre_item + ": " + PlayerPrefs.GetInt("cant" + itemsTiendaSO[numBoton].nombre_item));
                
                panelesTienda[numBoton].cantidad[PlayerPrefs.GetInt("cant" + itemsTiendaSO[numBoton].nombre_item) - 1].SetActive(true);
                
                coins = coins - itemsTiendaSO[numBoton].precio;
                coinUI.text = coins.ToString();
                IsComprable();
            }
            
            
        }

    }

    public void CargarItems()
    {
        for (int i = 0; i < itemsTiendaSO.Length; i++)
        {
            panelesTienda[i].nombre_item.text = itemsTiendaSO[i].nombre_item;
            panelesTienda[i].precio.text = itemsTiendaSO[i].precio.ToString();
        }
    }

    public void CargarCompras()
    {

        for (int i = 0; i < keyCompras.Length; i++)
        {

            if (PlayerPrefs.GetInt(keyCompras[i]) > 0)
            {
                Debug.Log(keyCompras[i] + ": " + PlayerPrefs.GetInt(keyCompras[i]));
                PlayerPrefs.SetInt(keyCompras[i], PlayerPrefs.GetInt(keyCompras[i]));
            }else
            {
                Debug.Log(keyCompras[i] + ": 0");
                PlayerPrefs.SetInt(keyCompras[i], 0);
            }

            
            if (PlayerPrefs.GetInt(keyCompras[i]) <= 7)
            {
                
                for (int j = 0; j < PlayerPrefs.GetInt(keyCompras[i]); j++)
                {
                    
                    panelesTienda[i].cantidad[j].SetActive(true);
                }
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
                for (int j = 0; j <= PlayerPrefs.GetInt(keyCompras[i]); j++)
                {
                    
                    panelesTienda[i].cantidad[j].SetActive(false);
                }
            }
            PlayerPrefs.DeleteKey(keyCompras[i]);
        }
    }
}
