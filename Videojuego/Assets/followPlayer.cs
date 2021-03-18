using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class followPlayer : MonoBehaviour
    {

    public GameObject seguir;
    public float maxCamX = 16.5f;
    public float minCamX = -9f;

    private float smooth = 0.15f;
    private float velocidad;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void fixedUpdate()
    {
        transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x,
            Mathf.Clamp(seguir.transform.position.x, minCamX, maxCamX),
            ref velocidad,smooth),transform.position.y,transform.position.z);
    }
}
