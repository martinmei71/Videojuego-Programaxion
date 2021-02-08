using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generadorEnemigosControler : MonoBehaviour
{

    public GameObject prefabEnemigoLento;
    public float intervalo = 2f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("generarEnemigo", 0f, intervalo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generarEnemigo()
    {
        Instantiate(prefabEnemigoLento, transform.position, Quaternion.identity);
    }
}
