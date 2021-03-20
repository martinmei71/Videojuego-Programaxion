using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioDeEscena : MonoBehaviour
{
    public GameObject Krakot;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.name=="Player")&& (Krakot == null))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
