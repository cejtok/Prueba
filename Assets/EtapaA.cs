using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class EtapaA : MonoBehaviour
{
    string jsonpath;
    //string jsontext;

    Estudiantes estudiantes;
    public GameObject prefabItemA;
    public GameObject Contenedor;
    public GameObject alertaA;
    public GameObject PanelEtapaB;
    public GameObject ScrollRectA;

    DateTime date;



    public void Verificar()
    {
        int i = 0;
        foreach (Transform child in Contenedor.transform)
        {
            i++;
            if (child.GetComponent<ItemA>().GetRevision() == false)
            {               
                Contenedor.SetActive(false);
                alertaA.SetActive(true);
                return;
            }
        }
        if (i > 0)
        {
            gameObject.SetActive(false);
            PanelEtapaB.SetActive(true);
        }
        

    }




    public void EsconderAlerta()
    {
        alertaA.SetActive(false);
        Contenedor.SetActive(true);
    }




    void readJSON()
    {
        date = File.GetLastWriteTime(jsonpath);

        try
        {
            estudiantes = JsonUtility.FromJson<Estudiantes>(File.ReadAllText(jsonpath));
            RectTransform rect = Contenedor.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, (120 + 20) * estudiantes.datos.Count);

            foreach (Transform child in Contenedor.transform)
            {
                Destroy(child.gameObject);
            }

            int i = 0;
            foreach (Estudiante e in estudiantes.datos)
            {
                GameObject item = Instantiate(prefabItemA, Contenedor.transform);
                RectTransform itemrect = item.GetComponent<RectTransform>();
                itemrect.localPosition = new Vector2(20, -(20 * (i + 1) + 120 * i));

                item.GetComponent<ItemA>().setEstudiante(e);
                i++;
            }
            return;
        }
        catch (IOException exception)
        {
            print(exception.ToString());
            print("iterar proceso de lectura");
            readJSON();
        }
        catch (Exception exception)
        {
            print(exception.ToString());
            print("fichero JSON erroneo");
            estudiantes = new Estudiantes();
            estudiantes.datos.Clear();

            foreach (Transform child in Contenedor.transform)
            {
                Destroy(child.gameObject);
            }
            return;
        }
        
    }




    // Start is called before the first frame update
    void Start()
    {
        jsonpath = Application.streamingAssetsPath + "/nombreJson.json"; //Application.dataPath + "\\StreamingAssets\\nombreJson.json"
    }

    // Update is called once per frame
    void Update()
    {
        ScrollRectA.GetComponent<ScrollRect>().verticalScrollbar.value += Input.GetAxis("Mouse ScrollWheel") * 0.5f; ;

        if (DateTime.Compare(date, File.GetLastWriteTime(jsonpath)) != 0)
        {
            readJSON();
        }
    }
}
