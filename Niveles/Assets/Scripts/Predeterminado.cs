
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Predeterminado : MonoBehaviour
{
    Quaternion rStart;
    Vector3 distancia;
    Vector3 pStart;
    GameObject jugador;
    GameObject bala1;
    public GameObject bala;
    public GameObject salidabala;
    NavMeshAgent navAgent;
    float rangoAtaque;
    float rangoVision;
    bool enemigo;
    bool atacando;
    int sala;
    float cadencia;
    float tiempoAcumulado;
    // Start is called before the first frame update
    void Start()
    {
        rStart = this.gameObject.transform.rotation;
        pStart = this.gameObject.transform.position;
        rangoVision = 10;
        rangoAtaque = 5;
        GetComponent<Vida>().vida = 10;
        jugador = GameObject.FindGameObjectWithTag("Jugador");
        navAgent = GetComponent<NavMeshAgent>();
        enemigo = false;
        sala = GetComponent<Vida>().sala;
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
        if (enemigo == false)
        {
            navAgent.SetDestination(pStart);
            if (Vector3.Distance(transform.position, pStart) < 2)
            {
                navAgent.isStopped = true;
                transform.rotation = rStart;
            }
        }
        else
        {

            navAgent.isStopped = false;

            navAgent.SetDestination(jugador.transform.position);

        }
    }
    private void Ataque()
    {
        if (Vector3.Distance(transform.position, jugador.transform.position) < rangoVision)
        {
            distancia = jugador.transform.position - transform.position;
            if (Vector3.Angle(transform.forward, distancia) < 80f)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, distancia, out hit, rangoVision))
                {

                    if (hit.transform.tag == "Jugador")
                    {
                        enemigo = true;

                        if (atacando == false)
                        {
                            if (Vector3.Distance(transform.position, jugador.transform.position) < rangoAtaque)
                            {
                                bala1 = GameObject.Instantiate(bala, salidabala.transform.position, transform.GetChild(1).rotation);

                                bala1.gameObject.GetComponent<Proyectil>().velocidad = 20;

                                bala1.gameObject.GetComponent<Proyectil>().daño = 2;

                                atacando = true;

                                cadencia = 1;
                            }
                        }
                    }
                    else
                    {
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
            }
        }
    }
}
