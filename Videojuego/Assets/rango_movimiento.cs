using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rango_movimiento : MonoBehaviour


{
    public Transform inicio;
    public Transform fin;

    private void OnDrawGizmosSelected()
    {
        if(inicio !=null && fin != null) // solo si tiene las variables valor
        {
            Gizmos.color = Color.blue; //selecionamos color azul para dibujar
            Gizmos.DrawLine(inicio.position, fin.position); //dibuja una linea, los parametrso son inicio desde coloso hasta donde queremos que vaya
            Gizmos.DrawWireSphere(fin.position, 0.1f); // dibuja un punto en la ´´ultima posición a la que llega

        }
    }

   
}
