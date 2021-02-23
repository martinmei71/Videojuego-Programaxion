using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [Range(0f, 0.20f)]
    public float parallaxSpeed = 0.02f;

    public RawImage background;
    public RawImage platform;
    public GameObject uiIdle;



    //LISTA DE ESTADOS  DE JUEGO
    public enum GameState { Idle, Playing, Ended};

    public GameState gameState = GameState.Idle;//gameState tiene por defecto idle


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Empieza el juego si el juego está parado y hacemos el click izquierda del ratón
        if (gameState==GameState.Idle && Input.GetMouseButtonDown(0))
        {
            gameState = GameState.Playing;
            //PARA DESACTIVAR LOS TITULOS
            uiIdle.SetActive(false);
        }
        //Juego en marcha
        else if (gameState == GameState.Playing)
        {
            Parallax();
        }
        else if (gameState == GameState.Ended)
        {
            if (Input.GetMouseButtonDown(0))
            {
                restartGame();
            }
        }

    void Parallax()
        {
            float finalSpeed = parallaxSpeed * Time.deltaTime;
            background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
            platform.uvRect = new Rect(background.uvRect.x + finalSpeed * 4f, 0f, 1f, 1f);
        }

       

    }
    public void restartGame()
    {
        SceneManager.LoadScene("SampleScene");
        }
}
