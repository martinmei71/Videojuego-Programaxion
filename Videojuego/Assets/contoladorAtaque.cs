using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contoladorAtaque : MonoBehaviour
{


    private Animator estado;

    // Start is called before the first frame update
    void Start()
    {
        estado = GetComponentInParent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("Enemy"))
        {
            if (estado.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            {
                collision.SendMessage("Muerto");


            }
        }
    }
}
