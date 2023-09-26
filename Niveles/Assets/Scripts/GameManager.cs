using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager Referencia;

    public static GameManager dameReferencia
    {
        get
        {
            if (Referencia == null)
            {
                GameObject nuevo = new GameObject("GameManager");
                nuevo.AddComponent<GameManager>();
                Referencia = nuevo.GetComponent<GameManager>();
            }
           
            return Referencia;
        }
    }

    public int moneda=0;
    public int llave;
    public int NivelsT=1;
    public int NivelsD=1;
    public int [] numeroEnemigos;
    GameObject puerta;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        numeroEnemigos = new int[7];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnemigoMuerto(int sala)
    {
        numeroEnemigos[sala]--;

        if (numeroEnemigos[sala] <= 0)
        {
            puerta = GameObject.FindGameObjectWithTag("Puerta " + sala);
            
            
            Destroy(puerta.gameObject,2);
        }
    }
    public void NumeroEnemigos(int sala)
    {
        numeroEnemigos[sala]++;
    }
    public void Muerte()
    {
        SceneManager.LoadScene("Muerte");
    }
    public void Win(int monedaP,int llaveP,int NivelT, int NivelD)
    {
        moneda = monedaP;
        llave = llaveP;
        NivelsD = NivelD;
        NivelsT = NivelT;


        SceneManager.LoadScene("Loby");
    }
    public void Siguiente(int monedaP, int llaveP, int NivelT, int NivelD)
    {
        moneda = monedaP;
        llave = llaveP;
        NivelsD = NivelD;
        NivelsT = NivelT;

        SceneManager.LoadScene("Juego");
    }
}
