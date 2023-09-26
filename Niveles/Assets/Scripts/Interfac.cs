using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Interfac : MonoBehaviour
{
    
    GameObject Personaje;
    float Salu;
    public GameObject Arm;
    public GameObject Vida;
    public GameObject Moneda;
    public GameObject Llave;
    // Start is called before the first frame update
    void Start()
    {
        Personaje = GameObject.FindGameObjectWithTag("Jugador");
    }

    // Update is called once per frame
    void Update()
    {
        Consumibles();
        Salud();
        Arma();
    }
    private void Consumibles()
    { 
        Moneda.GetComponent<Text>().text = Personaje.GetComponent<Personaje>().Monedas.ToString();
        Llave.GetComponent<Text>().text = Personaje.GetComponent<Personaje>().Llaves.ToString();
    }
    private void Salud()
    {
        Salu = Personaje.GetComponent<Vida>().vida;
        if (Salu == 8)
        {
            Vida.GetComponent<Image>().sprite = Resources.Load<Sprite>("Hood/8");
        }
        if (Salu == 7)
        {
            Vida.GetComponent<Image>().sprite = Resources.Load<Sprite>("Hood/7");
        }
        if (Personaje.GetComponent<Vida>().vida == 6)
        {
            Vida.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Hood/6");
        }
        if (Personaje.GetComponent<Vida>().vida == 5)
        {
            Vida.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Hood/5");
        }
        if (Personaje.GetComponent<Vida>().vida == 4)
        {
            Vida.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Hood/4");
        }
        if (Personaje.GetComponent<Vida>().vida == 3)
        {
            Vida.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Hood/3");
        }
        if (Personaje.GetComponent<Vida>().vida == 2)
        {
            Vida.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Hood/2");
        }
        if (Personaje.GetComponent<Vida>().vida == 1)
        {
            Vida.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Hood/1");
        }
        if (Personaje.GetComponent<Vida>().vida == 0)
        {
            Vida.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Hood/0");
        }
    }
    private void Arma()
    {
        if (Personaje.GetComponent<Personaje>().arma == 0)
        {
            Arm.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Hood/Daga");
        }
        if (Personaje.GetComponent<Personaje>().arma == 1)
        {
            Arm.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Hood/Tirachinas");
        }
    }
}
