using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vida : MonoBehaviour
{
    public float vida;
    public int escudo;
    public int sala;
    int orbe;
    public GameObject oSalud;
    public GameObject oVelocidad;
    public GameObject oEscudo;
    public GameObject oDaño;


    // Start is called before the first frame update
    void Start()
    {
        escudo = 0;
        if (this.gameObject.tag == "Enemigo")
        {
            GameManager.dameReferencia.NumeroEnemigos(sala);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Salud(float daño)
    {
        if (escudo > 0)
        {
            escudo--;
        }
        else
        {
            vida -= daño;
            if (vida <= 0)
            {
                if (this.gameObject.tag == "Enemigo")
                {
                    GameManager.dameReferencia.EnemigoMuerto(sala);

                    orbe = Random.Range(0, 9);

                    switch (orbe)
                    {
                        case 0:
                            GameObject.Instantiate(oSalud, transform.position, transform.rotation);
                            break;
                        case 1:
                            GameObject.Instantiate(oVelocidad, transform.position, transform.rotation);
                            break;
                        case 2:
                            GameObject.Instantiate(oEscudo, transform.position, transform.rotation);
                            break;
                        case 3:
                            GameObject.Instantiate(oDaño, transform.position, transform.rotation);
                            break;

                    }
                }
                if (this.gameObject.tag == "Jugador")
                {
                    GameManager.dameReferencia.Muerte();
                    Destroy(this.gameObject);
                }
                Destroy(this.gameObject);
            }
        }
        
    }
}
