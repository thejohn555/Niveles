using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogo : MonoBehaviour
{
    bool cerca;
    bool DialogoStart;
    public string[] Escrito;
    public GameObject Panel;
    public GameObject Izquierda;
    public GameObject Centro;
    public GameObject Derecha;
    public TMP_Text Texto;
    int lineaT;
    int Coste;


    // Update is called once per frame
    void Update()
    {
        if(cerca == true && Input.GetKeyDown(KeyCode.F))
        {
            if(!DialogoStart)
            {
                StartDialogue();
            }
            else if (Texto.text == Escrito[lineaT])
            {
                
                if (lineaT == 0)
                {
                    Izquierda.SetActive(true);
                    Centro.SetActive(true);
                    Derecha.SetActive(true);
                    Izquierda.transform.GetChild(0).GetComponent<TMP_Text>().text = "Daga";
                    Centro.transform.GetChild(0).GetComponent<TMP_Text>().text = "Tirachinas";
                    Derecha.transform.GetChild(0).GetComponent<TMP_Text>().text = "No";
                }
                if (lineaT == 3 || lineaT == 4 || lineaT == 2) 
                {
                    DialogoStart = false;
                }
            }
            else
            {
                StopAllCoroutines();
                Texto.text = Escrito[lineaT];
            }
        }
    }
    private void StartDialogue()
    {
        DialogoStart = true;
        lineaT = 0;
        StartCoroutine(MostrarLinea());
    }
    private void SiguienteLinea()
    {
        lineaT++;
        if (lineaT < Escrito.Length)
        {
            StartCoroutine(MostrarLinea());
        }
        else
        {
            DialogoStart = false;
        }
    }
    public void BotonIzquierdo()
    {
        if (Izquierda.transform.GetChild(0).GetComponent<TMP_Text>().text=="Daga")
        {
            MejoraDaga();
            
        }
        else if (Izquierda.transform.GetChild(0).GetComponent<TMP_Text>().text == "Si")
        {
            if (GameObject.FindGameObjectWithTag("Jugador").GetComponent<Personaje>().Monedas >= Coste)
            {
                if(GameObject.FindGameObjectWithTag("Jugador").GetComponent<Personaje>().NivelD <= 3)
                {
                    GameObject.FindGameObjectWithTag("Jugador").GetComponent<Personaje>().NivelD++;
                    GameObject.FindGameObjectWithTag("Jugador").GetComponent<Personaje>().Monedas= GameObject.FindGameObjectWithTag("Jugador").GetComponent<Personaje>().Monedas - Coste;
                    lineaT = 3;
                    StopAllCoroutines();
                    StartCoroutine(MostrarLinea());
                    Izquierda.SetActive(false);
                    Derecha.SetActive(false);
                }
                else
                {
                    lineaT = 4;
                    StopAllCoroutines();
                    StartCoroutine(MostrarLinea());
                    Izquierda.SetActive(false);
                    Derecha.SetActive(false);
                }
                
                
            }
            else
            {
                lineaT = 2;
                StopAllCoroutines();
                StartCoroutine(MostrarLinea());
                Izquierda.SetActive(false);
                Derecha.SetActive(false);
            }
        }
    }
    public void BotonCentral()
    {
        if (Centro.transform.GetChild(0).GetComponent<TMP_Text>().text == "Tirachinas")
        {
            MejoraTirachianas();
        }
        else if (Centro.transform.GetChild(0).GetComponent<TMP_Text>().text == "Si")
        {
            if (GameObject.FindGameObjectWithTag("Jugador").GetComponent<Personaje>().Monedas >= Coste)
            {
                if (GameObject.FindGameObjectWithTag("Jugador").GetComponent<Personaje>().NivelT <= 3)
                {
                    GameObject.FindGameObjectWithTag("Jugador").GetComponent<Personaje>().NivelT++;
                    GameObject.FindGameObjectWithTag("Jugador").GetComponent<Personaje>().Monedas = GameObject.FindGameObjectWithTag("Jugador").GetComponent<Personaje>().Monedas - Coste;
                    lineaT = 3;
                    StopAllCoroutines();
                    StartCoroutine(MostrarLinea());
                    Centro.SetActive(false);
                    Derecha.SetActive(false);
                }
                else
                {
                    lineaT = 4;
                    StopAllCoroutines();
                    StartCoroutine(MostrarLinea());
                    Centro.SetActive(false);
                    Derecha.SetActive(false);
                }


            }
            else
            {
                lineaT = 2;
                StopAllCoroutines();
                StartCoroutine(MostrarLinea());
                Centro.SetActive(false);
                Derecha.SetActive(false);
            }
        }
    }
    public void MejoraDaga()
    {
        Coste = 10;
        lineaT = 1;
        StopAllCoroutines();
        StartCoroutine(MostrarLinea());
        Centro.SetActive(false);
        Izquierda.transform.GetChild(0).GetComponent<TMP_Text>().text = "Si";
        Derecha.transform.GetChild(0).GetComponent<TMP_Text>().text = "No";

    }
    public void MejoraTirachianas()
    {
        Coste = 10;
        lineaT = 1;
        StopAllCoroutines();
        StartCoroutine(MostrarLinea());
        Izquierda.SetActive(false);
        Centro.transform.GetChild(0).GetComponent<TMP_Text>().text = "Si";
        Derecha.transform.GetChild(0).GetComponent<TMP_Text>().text = "No";
    }
    public void SalirDialogo()
    {
        DialogoStart = false;
        Izquierda.SetActive(false);
        Centro.SetActive(false);
        Derecha.SetActive(false);
    }
    IEnumerator MostrarLinea()
    {
        Texto.text = string.Empty;

        foreach(char ch in Escrito[lineaT])
        {
            Texto.text += ch;
            yield return new WaitForSeconds(0.05f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Jugador")
        {
            Panel.SetActive(true);
            cerca = true;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Jugador")
        {
            if (!DialogoStart)
            {
                Texto.text = "Pulsa F para interactuar";
                other.transform.GetComponent<Personaje>().enabled = true;
            }
            else
            {
                other.transform.GetComponent<Personaje>().enabled = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Jugador")
        {
            Panel.SetActive(false);
            cerca = false;
            DialogoStart = false;
        }
    }
}
