using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    GameObject jugador;
    float tiempoAcumulado;
    float cadencia;
    Vector3 direccion;
    Quaternion rotacion;
    Quaternion disparo;
    int giro;
    int rangoVision;
    bool atacando;
    bool enemigo;
    public GameObject bala;
    public GameObject balaTocha;
    public GameObject salidabala;
    GameObject bala1;
    int TipoAtaque;
    int nAtaques;

    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Jugador");
        GetComponent<Vida>().vida = 100;
        tiempoAcumulado = 0;
        cadencia = 0.5f;
        giro = 8;
        rangoVision = 15;
        atacando = false;
        TipoAtaque = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (jugador != null)
        {
            Ataque();
        }
            
    }
    private void Ataque()
    {
        switch (TipoAtaque)
        {
            case 0:
                Ataque1();
                break;
            case 1:
                Ataque2();
                break;
            case 2:
                Ataque3();
                break;
        }
    }
    private void Ataque1()
    {
        Mira();
        Disparo1();
    }
    private void Mira()
    {
        direccion = jugador.transform.position - transform.GetChild(0).position;

        

        if (Vector3.Distance(transform.GetChild(0).position, jugador.transform.position) < rangoVision)
        {
            
                RaycastHit hit;

                if (Physics.Raycast(transform.GetChild(0).position, direccion, out hit, rangoVision))
                {
                    Debug.Log("tumama");
                    if (hit.transform.tag == "Jugador")
                    {
                        rotacion = Quaternion.LookRotation(direccion.normalized, Vector3.up);

                        transform.GetChild(0).rotation = Quaternion.Lerp(transform.GetChild(0).rotation, rotacion, giro * Time.deltaTime);

                        direccion.y = 0;

                        rotacion = Quaternion.LookRotation(direccion.normalized, Vector3.up);

                        transform.rotation = Quaternion.Lerp(transform.rotation, rotacion, giro * Time.deltaTime);
                        enemigo = true;
                    }
                    else
                    {
                        enemigo = false;
                    }
                }
            
        }
        else
        {
            enemigo = false;
        }
    }
    private void Disparo1()
    {
        if (enemigo == true && atacando == false)
        {

            bala1 = GameObject.Instantiate(bala, salidabala.transform.position, transform.GetChild(1).rotation);

            bala1.gameObject.GetComponent<Proyectil>().velocidad = 20;

            bala1.gameObject.GetComponent<Proyectil>().daño = 1;

            atacando = true;

            cadencia = 1;

            nAtaques++;
            if (nAtaques > 5)
            {
                nAtaques = 0;
                TipoAtaque++;
            }
        }
        if (atacando == true)
        {

            tiempoAcumulado += Time.deltaTime;

            if (tiempoAcumulado > cadencia)
            {
                atacando = false;

                tiempoAcumulado = 0;
            }
        }
    }
    private void Ataque2()
    {
        if (Vector3.Distance(transform.position, jugador.transform.position) < rangoVision && atacando == false) 
        {
            disparo = Quaternion.LookRotation(-transform.GetChild(1).transform.right, Vector3.up);
            
            for(int i = 0; i < 12; i++)
            {
                disparo *= Quaternion.Euler(new Vector3(0, 15, 0));

                bala1 = GameObject.Instantiate(bala, salidabala.transform.position, disparo);

                bala1.gameObject.GetComponent<Proyectil>().velocidad = 20;

                bala1.gameObject.GetComponent<Proyectil>().daño = 1;

            }
            atacando = true;

            cadencia = 1;

            nAtaques++;

            if (nAtaques > 2)
            {
                nAtaques = 0;
                TipoAtaque++;
            }
        }
        if (atacando == true)
        {
            tiempoAcumulado += Time.deltaTime;

            if (tiempoAcumulado > cadencia)
            {
                atacando = false;

                tiempoAcumulado = 0;
            }
        }
    }
    private void Ataque3()
    {
        Mira();
        Disparo2();
    }
    private void Disparo2()
    {
        if (enemigo == true && atacando == false)
        {

            bala1 = GameObject.Instantiate(balaTocha, salidabala.transform.position, transform.GetChild(1).rotation);

            bala1.gameObject.GetComponent<BalaBoss>().velocidad = 20;

            bala1.gameObject.GetComponent<BalaBoss>().daño = 1;

            atacando = true;

            cadencia = 3;

            nAtaques++;
            if (nAtaques > 3)
            {
                nAtaques = 0;
                TipoAtaque = 0;
            }
        }
        if (atacando == true)
        {

            tiempoAcumulado += Time.deltaTime;

            if (tiempoAcumulado > cadencia)
            {
                atacando = false;

                tiempoAcumulado = 0;
            }
        }
    }

}
