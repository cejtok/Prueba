using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemB : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    RectTransform rect;
    Estudiante estudiante;
    public Text text_nombre;
    public Text text_nota;

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




    public void setEstudiante(Estudiante e)
    {
        estudiante = e;
        text_nombre.text = e.nombre + " " + e.apellido;
        text_nota.text = "" + e.nota;
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
