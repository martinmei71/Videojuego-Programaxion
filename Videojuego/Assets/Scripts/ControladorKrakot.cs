using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorKrakot : MonoBehaviour
{
    public float speed = 1f; // para variar velocidad desde fuera
    private float velocidadArreglada;
    public Transform target; // se le asocia el target que hemos dibujado
    private Vector3 inicio;
    private Vector3 fin; 
    private Animator estado;
    private BoxCollider2D rangoAtaque;

    // Start is called before the first frame update
    void Start()
    {
        
        estado = GetComponent<Animator>();
        rangoAtaque = GetComponent<BoxCollider2D>();
        target.parent = null; // desvincula Target de Coloso para que cuando se mueva coloso se mantenga la posición de target.
        inicio = transform.position; //posición incial de coloso.
        fin = target.position; // posición final del recorrido
        

    }

    // Update is called once per frame
    void  FixedUpdate()
    {
       if(target != null)
        {    //para mover el coloso de un punto a otrole pasamos las varaibles (posción actual de coloso, su targe y la velocidad multiplicada por la velocidad relativa de cada fotograma)

            if (estado.GetCurrentAnimatorStateInfo(0).IsName("Krakot_walk"))
            {
                velocidadArreglada = speed * Time.deltaTime;
            } else
            {
                velocidadArreglada = 0;
            }
      
           transform.position = Vector3.MoveTowards(transform.position, target.position, velocidadArreglada);
            
        }

       if(transform.position == target.position) // si ya llegó al final del recorrido
        {
            target.position = inicio; // intercambio de valores
            inicio = transform.position;
        }

       if(target.position.x > transform.position.x) // comprueba si la posición del target está a la dcha o izq de coloso
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
          }else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }


        if (estado.GetCurrentAnimatorStateInfo(0).IsName("Krakot_atack"))
        {
            rangoAtaque.enabled = true;
        }
        else
        {
            rangoAtaque.enabled = false;

        }


    }

    void OnTriggerEnter2D(Collider2D col)   
    {
        if (col.gameObject.tag == "Player")
        {
            col.SendMessage("Golpeado", transform.position.x);
        }
    }

    public void Muerto()
    {
        estado.SetTrigger("muerto");
        speed = 0f;
        gameObject.layer = 10;
        Invoke("DestruirObjeto", 2);


    }

    void DestruirObjeto()
    {
        Destroy(gameObject);
    }
}
