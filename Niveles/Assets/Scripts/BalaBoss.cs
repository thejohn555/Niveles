using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaBoss : MonoBehaviour
{
    public int daño;
    public int velocidad;
    int nAtaques;
    Quaternion disparo;
    public GameObject bala;
    GameObject bala1;
    float tiempoAcumulado;
    bool Stop;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        Boloncho();
        transform.Translate(transform.forward * velocidad * Time.deltaTime, Space.World);
    }
    private void Boloncho()
    {
        tiempoAcumulado += Time.deltaTime;
        if (tiempoAcumulado > 0.65f && Stop == false) 
        {
            disparo = Quaternion.LookRotation(transform.forward, Vector3.up);

            for (int i = 0; i < 25; i++)
            {
                disparo *= Quaternion.Euler(new Vector3(0, 15, 0));

                bala1 = GameObject.Instantiate(bala, transform.position, disparo);

                bala1.gameObject.GetComponent<Proyectil>().velocidad = 20;

                bala1.gameObject.GetComponent<Proyectil>().daño = 1;

            }

            nAtaques++;

            if (nAtaques > 2)
            {
                Stop = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemigo")
        {
            other.gameObject.GetComponent<Vida>().Salud(daño);
        }
        if (other.gameObject.tag == "Destruible")
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Jugador")
        {
            other.gameObject.GetComponent<Vida>().Salud(daño);
        }
        Destroy(this.gameObject);
    }
}
