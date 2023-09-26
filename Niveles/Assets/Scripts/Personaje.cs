using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    GameObject target;
    GameObject bala1;
    public GameObject bala;
    public GameObject salidabala;
    float Ver;
    float Hor;
    float tiempoAcumulado;
    float tiempoAcumulado1;
    float tiempoAcumulado2;
    float tiempoAcumulado3;
    float tiempoAcumulado4;
    float cadencia;
    float dPowerup;
    float dPowerup1;
    float dFinal;
    float Daño;
    public float carga;
    int Velocidad=6;
    int fuerzaSalto=170;
    int fuerzaDash = 2500;
    int giro = 6;
    public int NivelT;
    public int NivelD;
    public int Llaves;
    public int Monedas;
    public int arma;
    int armaMax;
    Vector3 reSpawn;
    Vector3 mov;
    Vector3 mira;
    Vector3 posInicial;
    Quaternion rotacion;
    Rigidbody rigibody;
    bool saltando = false;
    bool atacando = false;
    bool dash = false;
    bool Velocidadmas = false;
    bool dDaño;
    bool una;
    bool unavez = false;
    public Transform golpe;
    // Start is called before the first frame update
    void Start()
    {
        Llaves = GameManager.dameReferencia.llave;
        Monedas = GameManager.dameReferencia.moneda;
        NivelT = GameManager.dameReferencia.NivelsT;
        NivelD = GameManager.dameReferencia.NivelsD;
        una = true;
        Spawn();
        GetComponent<Vida>().vida = 8;
        arma = 0;
        target = transform.GetChild(0).gameObject;
        rigibody = GetComponent<Rigidbody>();
        armaMax = 0;
        Daño = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        Salto();
        Apuntado();
        Dash();
        TipoArma();
        Disparo();
        Cadencia();
        Pinchos();
    }
    public void Spawn()
    {
        reSpawn = transform.position;
    }

    private void Movimiento()
    {


        if (dash)
            return;
        
        if (saltando==true && Physics.Raycast(transform.position, transform.forward, 1.1f)|| saltando == true && Physics.Raycast(transform.position, transform.right, 1.1f)|| saltando == true && Physics.Raycast(transform.position, -transform.forward, 1.1f)|| saltando == true && Physics.Raycast(transform.position, -transform.right, 1.1f))
        {

        }
        else
        {
            Ver = Input.GetAxisRaw("Vertical");
            Hor = Input.GetAxisRaw("Horizontal");

            mov = (Hor * Vector3.forward + -Ver * Vector3.right).normalized * Velocidad;

            mov.y = rigibody.velocity.y;
            rigibody.velocity = mov;
            
        }
    }
    private void Salto()
    {
        if (saltando == true)
        {
            if (rigibody.velocity.y < 0)
            {
                RaycastHit hit1;
                if (Physics.Raycast(transform.position, -Vector3.up, out hit1, 2.1f))
                {
                    if (hit1.transform.tag == "suelo" )
                    {
                        saltando = false;
                        //rigibody.velocity = new Vector3(rigibody.velocity.x, 0, rigibody.velocity.z);
                    }
                    if (hit1.transform.tag == "Destruible")
                    {
                        saltando = false;
                        //rigibody.velocity = new Vector3(rigibody.velocity.x, 0, rigibody.velocity.z);
                    }
                }
            }
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up,out hit , 2.1f))
        {
            
        }
        else
        {
            saltando = true;
        }

            if (Input.GetKeyDown(KeyCode.Space) && saltando == false)
        {
            
            rigibody.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);

            saltando = true;
        }
    }
    private void Apuntado()
    {
        mira = target.transform.position - transform.position;
        rotacion = Quaternion.LookRotation(mira.normalized, Vector3.up);
        transform.GetChild(0).rotation = Quaternion.Lerp(transform.GetChild(0).rotation, rotacion, giro * Time.deltaTime);

        mira.y = 0;

        rotacion = Quaternion.LookRotation(mira.normalized, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotacion, giro * Time.deltaTime);
    }
    private void Dash()
    {
     
        if (unavez == true)
        {
            unavez = true;
            tiempoAcumulado1 += Time.deltaTime;

            if(tiempoAcumulado1 > 0.05f )
            {
                
                dash = false;
            }

            if (tiempoAcumulado1 > 1.5f)
            {
                dash = false;
                unavez = false;
                tiempoAcumulado1 = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && unavez == false)
        {
            rigibody.AddForce(transform.forward * fuerzaDash, ForceMode.Acceleration);
            dash = true;
            unavez = true;
        }
    }
    private void TipoArma()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            arma++;
            if (arma > armaMax)
            {
                arma = armaMax;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            arma--;
            if (arma < 0)
            {
                arma = 0;
            }
        }
    }
    private void Disparo()
    {
        switch (arma)
            {
                case 0:

                    Daga();

                    break;
                case 1:

                    Tirachinas();
                    
                    break;
            }

        
    }
    private void Daga()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && atacando == false)
        {
            Instantiate(golpe, transform.position+transform.forward*2, transform.rotation);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 2))
            {
                if (hit.transform.tag == "Enemigo")
                {
                    dFinal = 2 * Daño * NivelD;               
                    hit.transform.gameObject.GetComponent<Vida>().Salud(dFinal);
                }

            }
            atacando = true;
            cadencia = 0.5F;
        }

            
    }
    private void Tirachinas()
    {

        if (Input.GetKey(KeyCode.Mouse0) && atacando == false)
        {
            carga += Time.deltaTime;
            
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && atacando == false )
        {
            
            if (carga <= 1)
            {
                bala1 = GameObject.Instantiate(bala, salidabala.transform.position, transform.GetChild(1).rotation);

                bala1.gameObject.GetComponent<Proyectil>().velocidad = 10;

                bala1.gameObject.GetComponent<Proyectil>().daño = 1 * Daño * NivelT;

                atacando = true;

                cadencia = 1;
            }
            if (carga > 1 && carga <= 2)
            {
                bala1 = GameObject.Instantiate(bala, salidabala.transform.position, transform.GetChild(1).rotation);

                bala1.gameObject.GetComponent<Proyectil>().velocidad = 15;

                bala1.gameObject.GetComponent<Proyectil>().daño = 2 * Daño * NivelT;

                atacando = true;

                cadencia = 1;
            }
            if (carga > 2 )
            {
                bala1 = GameObject.Instantiate(bala, salidabala.transform.position, transform.GetChild(1).rotation);

                bala1.gameObject.GetComponent<Proyectil>().velocidad = 30;

                bala1.gameObject.GetComponent<Proyectil>().daño = 3 * Daño * NivelT;

                atacando = true;

                cadencia = 1;
            }
            carga = 0;
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
        if (Velocidadmas == true)
        {
            tiempoAcumulado2 += Time.deltaTime;

            if (tiempoAcumulado2 > dPowerup)
            {
                Velocidad -= 3;

                tiempoAcumulado2 = 0;

                Velocidadmas = false;
            }
        }
        if (dDaño == true)
        {
            tiempoAcumulado3 += Time.deltaTime;

            if (tiempoAcumulado3 > dPowerup1)
            {
                Daño = 1;

                tiempoAcumulado3 = 0;

                dDaño = false;
            }
        }
    }
    private void Pinchos()
    {
        if (una == false)
        {
            tiempoAcumulado4 += Time.deltaTime;
            if(tiempoAcumulado4 > 5)
            {
                una = true;
                tiempoAcumulado4 = 0;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Recoger Llaves
        if (collision.gameObject.tag == "Llave")
        {
            Llaves++;
            Destroy(collision.gameObject);
        }

        //Recoger Monedas
        if (collision.gameObject.tag == "Monedas")
        {
            Monedas++;
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //Apertura de cofre
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (other.gameObject.tag == "Cofre")
            {
                if (Llaves > 0)
                {
                    Monedas += Random.Range(5, 10);
                    Llaves--;
                    Destroy(other.gameObject,0.8f);
                }
            }
        }
        if (other.gameObject.tag == ("Pinchos"))
        {
            if (una == true)
            {
                Daño = 3;
                GetComponent<Vida>().Salud(Daño);
                una = false;

            }

        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Lava"))
        {
            Daño = 3;
            GetComponent<Vida>().Salud(Daño);
            transform.position = reSpawn;
            tiempoAcumulado = 0;
        }
        if (other.tag== "Spawn")
        {
            Spawn();
        }
        if (other.tag == "Arma")
        {
            armaMax++;
            Destroy(other.gameObject);
        }
        if (other.tag == "oSalud")
        {
            GetComponent<Vida>().vida += 2;
            Destroy(other.gameObject);
        }
        if (other.tag == "oVelocidad")
        {
            Velocidad += 3;
            dPowerup = 4;
            Velocidadmas = true;
            Destroy(other.gameObject);
        }
        if (other.tag == "oEscudo")
        {
            GetComponent<Vida>().escudo += 4;
            Destroy(other.gameObject);
        }
        if (other.tag == "oDaño")
        {
            Daño = 1.5f;
            dPowerup1 = 10;
            dDaño = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == ("Win"))
        {
            GameManager.dameReferencia.Win(Monedas, Llaves, NivelT, NivelD);
        }
        if (other.gameObject.tag == ("Siguiente"))
        {
            GameManager.dameReferencia.Siguiente(Monedas, Llaves, NivelT, NivelD);
        }
    }

}
