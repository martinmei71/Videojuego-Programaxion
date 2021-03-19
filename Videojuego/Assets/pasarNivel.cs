using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pasarNivel : MonoBehaviour
{

    public GameObject enemigos;
    public GameObject heroe;




    // Start is called before the first frame update

    private void Awake()
    {
        
        //DontDestroyOnLoad(this);

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemigos.transform.childCount == 0)
        {
            //heroe.SendMessage ("SumaVida");
            SceneManager.LoadScene("Scenes/Episode-2");
        } 
    }
}
