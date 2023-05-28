using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriterenderer;
    [SerializeField] private float velocidad = 0.01f;
    [SerializeField] private int coins = 0;
    [SerializeField] private int HP = 6;

    [SerializeField] private AudioSource audiosource;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip dodgeSound;
    [SerializeField] private AudioClip damageSound;


    // Start is called before the first frame update
    void Start(){
        //coinSound = GetComponent<>
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){
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
        
    }

    void OnCollisionStay2D(Collision2D other) {

        if (other.gameObject.CompareTag("Pasto")){
            velocidad = 0.01f;
        }

        if (other.gameObject.CompareTag("Petroleo")){
            velocidad = 0.001f;
        }

        if(other.gameObject.CompareTag("Hielo")){
            velocidad = 0.02f;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("coin")) {
            Destroy(other.gameObject);
            audiosource.clip = coinSound;
            audiosource.Play();
            coins++;
            var HudManager = FindObjectOfType<hudManager>();
            HudManager.AddMoney(coins);
        }
        if (other.gameObject.CompareTag("Enemy")) {
            if (Input.GetKey(KeyCode.UpArrow)){
                audiosource.clip = dodgeSound;
                audiosource.Play();
            }
            else{
                audiosource.clip = damageSound;
                audiosource.Play();
                velocidad = velocidad*0.01f;
                spriterenderer.color = new Color (1, 0, 0, 1); 

            }
        }
        if (other.gameObject.CompareTag("Disparo")) {
            Destroy(other.gameObject);
            audiosource.clip = damageSound;
            HP = HP - 1;

        }
    }
    void OnTriggerExit2D(Collider2D other){
        spriterenderer.color = new Color (1, 1, 1, 1); 
    }
}