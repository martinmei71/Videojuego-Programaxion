using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour {

    public float m_speed = 1.5f;
    public float m_jumpForce = 5.0f;
    public bool m_noBlood = false;
    
    public GameObject game;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_HeroKnight   m_groundSensor;
   
    private bool                m_grounded = false;
    bool herido = false;

    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
   

    private float               m_delayToIdle = 0.0f;


    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        
        //SIRVE PARA DETECTAR EL ESTADO DEL PERSONAJE Y DARLE MOVIMIENTO
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
       
    }

    // Update is called once per frame
    void Update ()
    {
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;
        

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            transform.localScale = new Vector3(0.3642007f, 0.3642007f, 0.3642007f);
            // m_facingDirection = 1;
        }
            
        else if (inputX < 0)
        {
            transform.localScale = new Vector3(-0.3642007f, 0.3642007f, 0.3642007f);
            //m_facingDirection = -1;
        }

        // Move
        if (!herido)
        {
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        }

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

       


        //MUERTE---------------------------------------------------------------------
        //Death
        if (Input.GetKeyDown("r"))
        {
          
            m_animator.SetTrigger("Death");
        }
        //HERIDA---------------------------------------------------------------------
        //Hurt
        else if (Input.GetKeyDown("q"))
            m_animator.SetTrigger("Hurt");




        //Attack
        //TIEMPO QUE TARDA EN ATACAR 0.4
        else if ((Input.GetKeyDown("up") || Input.GetKeyDown("e")) && m_timeSinceAttack > 0.40f)
        {

            m_currentAttack = 1;

            // Call attack animations "Attack1"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }


        //Jump
        else if ((Input.GetKeyDown("space") || Input.GetKeyDown("w")) && m_grounded)
        {
            jump();
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
                if(m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
        }
    }

    public void jump()
    {
        m_animator.SetTrigger("Jump");
        m_grounded = false;
        m_animator.SetBool("Grounded", m_grounded);
        m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
        m_groundSensor.Disable(0.2f);
    }



    public void Golpeado(float posicionEnemigoX)
    {
        herido = true;
        m_animator.SetTrigger("Hurt");
        float side = Mathf.Sign(posicionEnemigoX - transform.position.x);


        //añadir impulso
        m_body2d.AddForce(Vector2.left*side*5f,ForceMode2D.Impulse);

        jump();
        Invoke("trapa", 1f);

        

        //RESTAR VIDA!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        
    }

    public void trapa()
    {
        herido = false;
    }


    //metodo para colisiones con enemigos---------------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //UpdateState();
            //PARA EL REINICO
            game.GetComponent<GameController>().gameState = GameController.GameState.Ended;
        }
    }


}
