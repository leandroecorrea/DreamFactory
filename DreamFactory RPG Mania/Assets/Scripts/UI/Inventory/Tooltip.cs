using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{    

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"Pointer enter on position {eventData.position}");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"Pointer exit on position {eventData.position}");
    }
}
