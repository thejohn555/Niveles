using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    Transform Jugador;
    // Start is called before the first frame update
    void Start()
    {
        Jugador = GameObject.FindGameObjectWithTag("Jugador").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Jugador != null)
        {
            transform.position = new Vector3(Jugador.position.x + 6, transform.position.y, Jugador.position.z);
        }
            
    }
}
