using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescripcionConMouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject panelDescripcion;

    public void OnPointerEnter(PointerEventData eventData)
    {
        panelDescripcion.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        panelDescripcion.SetActive(false);        
    }
  
}
