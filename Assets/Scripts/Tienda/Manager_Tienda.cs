using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {

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
    /////////////////////////////////////////////////////////////////////
    //Este metodo es solo para testing, no se implementa en el juego real
    public void AddCoins()
    {
        coins = PlayerPrefs.GetInt("shop_coins") + PlayerPrefs.GetInt("run_coins");
        coinUI.text = coins.ToString();
        IsComprable();
    }
    /////////////////////////////////////////////////////////////////////

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
            coins = coins - itemsTiendaSO[numBoton].precio;
            coinUI.text = coins.ToString();
            IsComprable();
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

}
