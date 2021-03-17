using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorColoso : MonoBehaviour
{
    public float speed = 1f; // para variar velocidad desde fuera
    private float velocidadArreglada;
    public Transform target; // se le asocia el target que hemos dibujado
    private Vector3 inicio;
    private Vector3 fin;
    private SpriteRenderer flip; // para detectar cuando dar la vuelta al recorrido
    private Animator estado;

    





    // Start is called before the first frame update
    void Start()
    {
        flip = GetComponent<SpriteRenderer>();
        estado = GetComponent<Animator>();
        target.parent = null; // desvincula Target de Coloso para que cuando se mueva coloso se mantenga la posición de target.
        inicio = transform.position; //posición incial de coloso.
        fin = target.position; // posición final del recorrido
    }

    // Update is called once per frame
    void  FixedUpdate()
    {
       if(target != null)
        {    //para mover el coloso de un punto a otrole pasamos las varaibles (posción actual de coloso, su targe y la velocidad multiplicada por la velocidad relativa de cada fotograma)
           velocidadArreglada = speed * Time.deltaTime;
           transform.position = Vector3.MoveTowards(transform.position, target.position, velocidadArreglada);  
        }

       if(transform.position == target.position) // si ya llegó al final del recorrido
        {
            target.position = inicio; // intercambio de valores
            inicio = transform.position;
        }

       if(target.position.x > transform.position.x) // comprueba si la posición del target está a la dcha o izq de coloso
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            //flip.flipX = false;

        }else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
           // flip.flipX = true;
        }

                 

    }

     void OnTriggerEnter2D(Collider2D col)
    {
       if(col.gameObject.tag == "Player")
        {
            col.SendMessage("Golpeado", transform.position.x);
        }
    }

    public void Muerto()
    {
        estado.SetTrigger("muerto");
        speed = 0f;
        Invoke("DestruirObjeto", 2);

        
    }

    void DestruirObjeto()
    {
        Destroy(gameObject);
    }


}
