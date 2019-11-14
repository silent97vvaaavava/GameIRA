using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CheckPoint : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.pointerPressRaycast.gameObject.name == "MarginBorder")
        Debug.Log(Camera.main.ScreenToWorldPoint(eventData.position));
    }

   
}
