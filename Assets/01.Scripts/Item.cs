using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(MyBoolArray))]
public class Item : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    private MyBoolArray grid;
    private Transform trm => transform as RectTransform;
    
    
    private void Start()
    {
        grid = GetComponent<MyBoolArray>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        trm.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        trm.position = eventData.position;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    
}
