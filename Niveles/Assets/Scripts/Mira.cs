using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mira : MonoBehaviour
{
    Vector3 mira;
    Plane plane = new Plane(Vector3.up, 0);
    GameObject jugador;

    // Update is called once per frame
    void OnGUI()
    {
        jugador = GameObject.FindGameObjectWithTag("Jugador");
        float distancia;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(plane.Raycast(ray,out distancia))
        {
            mira = ray.GetPoint(distancia);
        }
        mira.y = jugador.gameObject.transform.position.y;
        transform.position = mira;
    }
}
