using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemB : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    
    public void OnBeginDrag(PointerEventData eventdata)
    {

    }

    public void OnEndDrag(PointerEventData eventdata)
    {

    }

    public void OnDrag(PointerEventData eventdata)
    {
        rect.anchoredPosition += eventdata.delta;
    }

    public void OnPointerDown(PointerEventData eventdata)
    {

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
