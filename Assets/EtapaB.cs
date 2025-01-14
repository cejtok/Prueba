﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class EtapaB : MonoBehaviour
{
    public GameObject col1;
    public GameObject col2;
    public GameObject col3;
    public GameObject Contenedor;
    public GameObject prefabItemB;
    public GameObject alertaB;
    public GameObject TextAlertaB;
    public GameObject ScrollRectB;


    string jsonpath;
    Estudiantes estudiantes;
    DateTime date;

    float x1;
    float x2;
    float x3;


    void readJSON()
    {
        date = File.GetLastWriteTime(jsonpath);
        print("lectura de json");

        try
        {
            estudiantes = JsonUtility.FromJson<Estudiantes>(File.ReadAllText(jsonpath));
            RectTransform rect = Contenedor.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, (70 + 5) * estudiantes.datos.Count);

            print(estudiantes.datos.Count);

            foreach (Transform child in Contenedor.transform)
            {
                if (!(child.gameObject.Equals(col1) || child.gameObject.Equals(col2) || child.gameObject.Equals(col3))) {
                    Destroy(child.gameObject);
                }                
            }

            int i = 0;
            foreach (Estudiante e in estudiantes.datos)
            {
                GameObject item = Instantiate(prefabItemB, Contenedor.transform);
                RectTransform itemrect = item.GetComponent<RectTransform>();

                itemrect.localPosition = new Vector2(-200, -(5 * (i + 1) + 70 * i));

                item.GetComponent<ItemB>().setEstudiante(e);
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
                if (!(child.gameObject.Equals(col1) || child.gameObject.Equals(col2) || child.gameObject.Equals(col3)))
                {
                    Destroy(child.gameObject);
                }
            }

            return;
        }
        

    }





    // Start is called before the first frame update
    void Start()
    {
        jsonpath = Application.streamingAssetsPath + "/nombreJson.json";//
    }

    // Update is called once per frame
    void Update()
    {
        ScrollRectB.GetComponent<ScrollRect>().verticalScrollbar.value += Input.GetAxis("Mouse ScrollWheel") * 1.0f;

        //print("...");
        if (DateTime.Compare(date, File.GetLastWriteTime(jsonpath)) != 0)
        {
            readJSON();
        }
    }


    public void Alerta(string msg)
    {
        print(msg);
        Contenedor.SetActive(false);
        alertaB.SetActive(true);
        TextAlertaB.GetComponent<Text>().text = msg;
    }


    public void EsconderAlerta()
    {
        Contenedor.SetActive(true);
        alertaB.SetActive(false);
    }


}
