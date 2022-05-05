using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemA : MonoBehaviour
{

    public GameObject aprobado;
    public Text text;
    public float nota;
    public Estudiante estudiante;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setEstudiante(Estudiante e)
    {
        estudiante = e;
        text.text = "Nombre: " + e.nombre + "\nApellido: " + e.apellido + "\nEdad: " + e.edad + "\nCodigo: " + e.codigo + "\nCorreo: " + e.correo + "\nNota final: " + e.nota;
    }

    public bool GetRevision()
    {        
        return (estudiante.nota >= 3.0f) == aprobado.GetComponent<Toggle>().isOn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
