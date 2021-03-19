using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoMovimiento : MonoBehaviour
{
    public Transform inicio;
    public Transform fin;

    private void OnDrawGizmosSelected()
    {
        if(inicio !=null && fin != null)
        {
            Gizmos.color = Color.blue; // selecionamos color azul para dibujar.
            Gizmos.DrawLine(inicio.position, fin.position);  //dibuja una linea y le damos los parametros de incio y de fin
            Gizmos.DrawWireSphere(fin.position, 0.1f); // dibuja un punto en la última posición a la que llega
        }
    }
}
