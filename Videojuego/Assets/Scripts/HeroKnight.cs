using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


using System.Collections;

public class HeroKnight : MonoBehaviour {

    public float m_speed = 1.5f;
    public float m_jumpForce = 5.0f;
    public bool m_noBlood = false;
    
    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_HeroKnight   m_groundSensor;
   
    private bool                m_grounded = false;
    bool herido = false;

    //private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
   

    private float               m_delayToIdle = 0.0f;
    private float inputX;


    //CONTADOR VIDAS
    public Text textoVidas;
    private int vidas;
    public GameObject enemigos;
    bool finVida = true;
    private GameObject almacenVidas;
    private Text vidasIniciales;

    public Text textoGameOver;

    //Scene escena;

    

    // Use this for initialization
    void Start ()
    {
        almacenVidas = GameObject.FindGameObjectWithTag("AlmacenVidas");
        vidasIniciales = almacenVidas.GetComponent<Text>();
        textoVidas.text = vidasIniciales.text;
        vidas = int.Parse(textoVidas.text);




        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        
        //SIRVE PARA DETECTAR EL ESTADO DEL PERSONAJE Y DARLE MOVIMIENTO
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();

        textoVidas.text = vidas.ToString(); 

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
        if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            inputX = Input.GetAxis("Horizontal");


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
                if (m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
            }
        }

        
        if (enemigos.transform.childCount == 0 && finVida) // comprueba si ya no quedan enemigos 
        {
            textoVidas.text = (++vidas).ToString();  // suma una vida
            finVida = false;
        }

        if (almacenVidas != null) vidasIniciales.text = vidas.ToString();

    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ("Fruta")) // si colisiona con la fruta

        {
            textoVidas.text = (++vidas).ToString();  // suma una vida
            collision.transform.position = new Vector3(0f, 0f, -1f); // cambia de capa el objeto
            Destroy(collision); // deshabilita  la colisión
        }
    }

    //MÉTODO SALTAR
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
        //PRIMERO RESTO EN VIDAS Y DESPUÉS LO PASO A STRING
        textoVidas.text = (--vidas).ToString();
        if (vidas<=0)
        {
            m_animator.SetTrigger("Death");
            m_body2d.velocity = new Vector2(0f, 0f);        // cambia la velocidad a 0
            gameObject.layer = 10;                         // desactiva colisiones (layer "noColision")
            Destroy(almacenVidas);
            textoGameOver.enabled = true;
            Invoke ("GameOver" , 4f);
        }
        else
        {
            herido = true;

            m_animator.SetTrigger("Hurt");
            float side = Mathf.Sign(posicionEnemigoX - transform.position.x);


            //añadir impulso
            m_body2d.AddForce(Vector2.left * side * m_jumpForce/2, ForceMode2D.Impulse);

            jump();
            Invoke("Curarse", 1f);
        }
      
        
    }

    //VUELVE AL PRINCIPIO DEL JUEGO
    public void GameOver()
    {
        SceneManager.LoadScene("MainTitle");
    }

    //MÉTODO PARA HACER HERIDO FALSE
    public void Curarse()
    {
        herido = false;
    }

  

}
