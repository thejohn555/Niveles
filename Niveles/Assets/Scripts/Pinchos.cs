using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinchos : MonoBehaviour
{

    float cuenta;
    bool encendido;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cuenta += Time.deltaTime;

        if (cuenta > 5)
        {
            GetComponent<BoxCollider>().enabled = !GetComponent<BoxCollider>().enabled;
            GetComponent<MeshRenderer>().enabled = !GetComponent<MeshRenderer>().enabled;
            cuenta = 0;
        }
    }
}
