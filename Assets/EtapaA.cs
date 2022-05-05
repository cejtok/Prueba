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

    DateTime date;



    public void Verificar()
    {
        
        foreach (Transform child in Contenedor.transform)
        {
            if(child.GetComponent<ItemA>().GetRevision() == false)
            {
                Contenedor.SetActive(false);
                alertaA.SetActive(true);
                return;
            }
        }

    }




    public void EsconderAlerta()
    {
        alertaA.SetActive(false);
        Contenedor.SetActive(true);
    }




    bool readJSON()
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
            return true;
        }
        catch(Exception exception)
        {

            print(exception.ToString());
            estudiantes = new Estudiantes();
            estudiantes.datos.Clear();

            foreach (Transform child in Contenedor.transform)
            {
                Destroy(child.gameObject);
            }

            return false;
        }
        
    }




    // Start is called before the first frame update
    void Start()
    {
        jsonpath = Application.dataPath + "\\StreamingAssets\\nombreJson.json";
    }

    // Update is called once per frame
    void Update()
    {
        if (!date.Equals(File.GetLastWriteTime(jsonpath)))
        {
            readJSON();
        }
    }
}
