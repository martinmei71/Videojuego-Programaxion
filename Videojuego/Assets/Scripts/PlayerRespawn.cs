using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerRespawn : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //LLAMAR DESDE LOS ENEMIGOS
    public void PlayerDamaged()
    {
        animator.Play("Hurt");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
