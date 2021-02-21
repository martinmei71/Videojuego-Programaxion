using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorColoso : MonoBehaviour
{
    public float speed = 1f; // para variar velocidad desde fuera
    public Transform target; // se le asocia el target que hemos dibujado
    private Vector3 inicio;
    private Vector3 fin;
    private SpriteRenderer flip; // para detectar cuando dar la vuelta al recorrido

    private bool enRango = false;
    private bool muerto = false;





    // Start is called before the first frame update
    void Start()
    {
        flip = GetComponent<SpriteRenderer>();
        target.parent = null; // desvincula Target de Coloso para que cuando se mueva coloso se mantenga la posición de target.
        inicio = transform.position; //posición incial de coloso.
        fin = target.position; // posición final del recorrido
        

    }

    // Update is called once per frame
    void  FixedUpdate()
    {
       if(target != null)
        {    //para mover el coloso de un punto a otrole pasamos las varaibles (posción actual de coloso, su targe y la velocidad multiplicada por la velocidad relativa de cada fotograma)
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
           
        }

       if(transform.position == target.position) // si ya llegó al final del recorrido
        {
            target.position = inicio; // intercambio de valores
            inicio = transform.position;
        }

       if(target.position.x > transform.position.x) // comprueba si la posición del target está a la dcha o izq de coloso
        {
            flip.flipX = false;

        }else
        {
            flip.flipX = true;
        }
        


    }
}
