using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public float velocidad = 0.01f;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento a la izquierda
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Translate(-velocidad, 0, 0);
            animator.SetBool("Izquierda", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            animator.SetBool("Izquierda", false);
        }
        //Movimiento a la derecha
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Translate(velocidad, 0, 0);
            animator.SetBool("Derecha", true);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            animator.SetBool("Derecha", false);
        }
        //Movimiento hacia arriba
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.Translate(0, velocidad, 0);
        }
        //Movimiento hacia abajo
        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.Translate(0, -velocidad, 0);
        }
    }
}
