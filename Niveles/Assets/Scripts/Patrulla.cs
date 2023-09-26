using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrulla : MonoBehaviour
{
    Vector3 distancia;
    GameObject jugador;
    NavMeshAgent navAgent;
    GameObject pRuta;
    GameObject[] psRutas;
    int i;
    public float velocidad;
    public float rangoAtaque;
    public float rangoVision;
    bool enemigo;
    bool atacando;
    int sala;
    public int Daño;
    float cadencia;
    float tiempoAcumulado;

    // Start is called before the first frame update
    void Start()
    {
        
        
        i = 0;
        GetComponent<Vida>().vida=10;
        jugador = GameObject.FindGameObjectWithTag("Jugador");
        navAgent = GetComponent<NavMeshAgent>();
        sala = GetComponent<Vida>().sala;
        psRutas = GameObject.FindGameObjectsWithTag("Punto de ruta " + sala);
        pRuta = psRutas[i];
        enemigo = false;
        navAgent.speed = velocidad;
    }

    // Update is called once per frame
    void Update()
    {
        if (jugador != null)
        {
            Movimiento();
            Ataque();
            Cadencia();
        }

    }
    private void Movimiento()
    {
        navAgent.SetDestination(pRuta.transform.position);

        if (enemigo == false)
        {
            pRuta = psRutas[i];



            if (Vector3.Distance(transform.position, pRuta.transform.position) < 2f)
            {
                i++;
                if (i >= psRutas.Length)
                {
                    i = 0;
                }
            }
        }
        else
        {
            pRuta = jugador;


        }
    }
    private void Ataque()
    {
        if (Vector3.Distance(transform.position, jugador.transform.position) < rangoVision)
        {
            distancia = jugador.transform.position - transform.position;
            if (Vector3.Angle(transform.forward, distancia) < 180f)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.GetChild(0).position, distancia, out hit, rangoVision))
                {

                    if (hit.transform.tag == "Jugador")
                    {
                        enemigo = true;

                        if (atacando == false)
                        {
                            if (Vector3.Distance(transform.position, jugador.transform.position) < rangoAtaque)
                            {
                                if (Physics.Raycast(transform.GetChild(0).position, transform.forward, out hit, 2))
                                {
                                    Debug.Log("ataco");
                                    hit.transform.gameObject.GetComponent<Vida>().Salud(Daño);
                                    atacando = true;
                                    cadencia = 1F;
                                    navAgent.isStopped = true;
                                }
                            }                            
                        }
                    }
                    else
                    {
                        pRuta = psRutas[i];

                        enemigo = false;
                    }
                }
            }
        }
    }
    private void Cadencia()
    {
        if (atacando == true)
        {

            tiempoAcumulado += Time.deltaTime;

            if (tiempoAcumulado > cadencia)
            {
                
                atacando = false;

                tiempoAcumulado = 0;

                navAgent.isStopped = false;
            }
        }
    }
}
