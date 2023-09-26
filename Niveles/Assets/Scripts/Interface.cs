using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interface : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PulsoBoton(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
    public void Salir(bool nombre)
    {
        if (nombre == true)
        {
            Application.Quit();
        }
        
    }
}
