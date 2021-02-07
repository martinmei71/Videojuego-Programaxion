using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlador_enemigo_lento : MonoBehaviour
{
    public float speed = -2f;
    private Rigidbody2D enemigo;
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()

    {
        enemigo = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
      
    }

    // Update is called once per frame
    void Update()
    {
        enemigo.velocity = new Vector2(speed, enemigo.velocity.y);
       
    }
}
