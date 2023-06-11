using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    [SerializeField] private GameObject jugador;
    [SerializeField] private Vector2 minimo;
    [SerializeField] private Vector2 maximo;
    [SerializeField] private float suavizado;
    [SerializeField] private Vector2 velocity;


    // Start is called before the first frame update
    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, jugador.transform.position.x, ref velocity.x, suavizado);
        float posY = Mathf.SmoothDamp(transform.position.y, jugador.transform.position.y, ref velocity.y, suavizado);
        transform.position = new Vector3(Mathf.Clamp(posX,minimo.x,maximo.x),Mathf.Clamp(posY,minimo.y,maximo.y), transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
