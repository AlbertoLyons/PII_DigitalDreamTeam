using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class hudManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMoneyCount; //serializefield hace que el valor privado se vea en el unity
    [SerializeField] private int _moneyCount;
   

    
    public void AddMoney(int value)
    {

        _moneyCount = value;

        _textMoneyCount.text = "" + _moneyCount;

    }
}
