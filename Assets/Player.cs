using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriterenderer;
    public float velocidad = 0.01f;
    public int coins = 0;

    public AudioSource audiosource;
    public AudioClip coinSound;
    public AudioClip dodgeSound;
    

    // Start is called before the first frame update
    void Start()
    {
        //coinSound = GetComponent<>
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento a la izquierda
        if (Input.GetKey(KeyCode.LeftArrow)) {
            spriterenderer.flipX = true;
            transform.Translate(-velocidad, 0, 0);
            animator.SetBool("Derecha", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            animator.SetBool("Derecha", false);
        }
        //Movimiento a la derecha
        if (Input.GetKey(KeyCode.RightArrow)) {
            spriterenderer.flipX = false;
            transform.Translate(velocidad, 0, 0);
            animator.SetBool("Derecha", true);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            animator.SetBool("Derecha", false);
        }
        /*Movimiento hacia arriba
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.Translate(0, velocidad, 0);
        }
        //Movimiento hacia abajo
        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.Translate(0, -velocidad, 0);
        }
        */

        
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("coin")) {
            Destroy(other.gameObject);
            audiosource.clip = coinSound;
            audiosource.Play();
            coins++;

        }
        if (other.gameObject.CompareTag("Enemy")) {
            if (Input.GetKey(KeyCode.UpArrow)){
                audiosource.clip = dodgeSound;
                audiosource.Play();
            }

        }

    }
}
