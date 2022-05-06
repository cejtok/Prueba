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

    public GameObject Alerta;
    public Text text_alerta;


    int col = 0;
    Vector2 pos;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    
    public void OnBeginDrag(PointerEventData eventdata)
    {
        pos = rect.anchoredPosition;
    }

    public bool getRevision()
    {
        if (col == 1)
        {
            return estudiante.nota >= 3.0f;
        }
        else
        {
            if (col == 2)
            {
                return estudiante.nota < 3.0f;
            }
            else
            {
                return false;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventdata)
    {
        int newcol = Mathf.Clamp(Mathf.FloorToInt((rect.anchoredPosition.x+100) / 200.0f) + 1, 0, 2);       

        if (newcol != col)
        {
            col = newcol;
            tag = "col" + newcol;

            rect.anchoredPosition = new Vector2((newcol - 1) * 200, rect.anchoredPosition.y);

            GameObject[] list0 = GameObject.FindGameObjectsWithTag("col0");
            GameObject[] list1 = GameObject.FindGameObjectsWithTag("col1");
            GameObject[] list2 = GameObject.FindGameObjectsWithTag("col2");
            int i = 0;
            RectTransform rectg;
            foreach (GameObject obj in list0) {
                rectg = obj.GetComponent<RectTransform>();
                rectg.anchoredPosition = new Vector2(rectg.anchoredPosition.x, -(5 * (i + 1) + 70 * i));
                i++;
            }
            i = 0;
            foreach (GameObject obj in list1)
            {
                rectg = obj.GetComponent<RectTransform>();
                rectg.anchoredPosition = new Vector2(rectg.anchoredPosition.x, -(5 * (i + 1) + 70 * i));
                i++;
            }
            i = 0;
            foreach (GameObject obj in list2)
            {
                rectg = obj.GetComponent<RectTransform>();
                rectg.anchoredPosition = new Vector2(rectg.anchoredPosition.x, -(5 * (i + 1) + 70 * i));
                i++;
            }

            if((list0.Length == 0)&&((list1.Length>0)||(list2.Length>0)))
            {
                bool check = true;
                foreach (GameObject obj in list1)
                {
                    if (obj.GetComponent<ItemB>().getRevision() == false)
                    {
                        check = false;
                        break;
                    }                        
                }
                foreach (GameObject obj in list2)
                {
                    if (obj.GetComponent<ItemB>().getRevision() == false)
                    {
                        check = false;
                        break;
                    }
                }
                GameObject o = GameObject.Find("ScrollRectB");
                Text textalerta = GameObject.Find("TextAlertaB").GetComponent<Text>();
                o.SetActive(false);

                if (check == true)
                {
                    textalerta.text = "PERFECTO:\nLos estudiantes estan ubicados correctamente";
                }
                else
                {
                    textalerta.text = "FALLASTE:\nLos estudiantes estan ubicados incorrectamente";
                }
            }

        }
        else
        {
            rect.anchoredPosition = pos;
        }
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
        tag = "col0";
        col = 0;
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
