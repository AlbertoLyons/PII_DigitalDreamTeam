using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriterenderer;

    public static float velocidad = 0.1f;
    [SerializeField] private int coins = 0;
    //[SerializeField] private int HP = 6;
    [SerializeField] private int countShield = 0;
    [SerializeField] private int multiplicador = 1;
    
    [SerializeField] private AudioSource audiosource;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip dodgeSound;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip shieldSound;
    [SerializeField] private AudioClip fastBoots;
    [SerializeField] private AudioClip slowBoots;
    [SerializeField] private AudioClip time;
    [SerializeField] private AudioClip xCoins;
    [SerializeField] private AudioClip Ralentizacion;

    [SerializeField] private Shield_UI escudo;
    [SerializeField] private Multiplier_UI multiplicadorUI;
    //[SerializeField] private GameObject particulas;
    // Start is called before the first frame update
    void Start(){
        velocidad = 0.1f;
        countShield = 0;
        //coinSound = GetComponent<>
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){
        //particulaMovimiento.transform.Translate(transform.position.x,transform.position.y,0);
        if(this != null){
            //Movimiento a la izquierda
            if (Input.GetKey(KeyCode.LeftArrow)) {
                spriterenderer.flipX = true;
                //particulas.SetActive(true);
                transform.Translate(-velocidad, 0, 0);
                animator.SetBool("Derecha", true);
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow)) {
                animator.SetBool("Derecha", false);
                
            }
            //Movimiento a la derecha
            if (Input.GetKey(KeyCode.RightArrow)) {
                spriterenderer.flipX = false;
                //particulas.SetActive(true);
                transform.Translate(velocidad, 0, 0);
                animator.SetBool("Derecha", true);
            }
            if (Input.GetKeyUp(KeyCode.RightArrow)) {
                animator.SetBool("Derecha", false);
                
            }
            /*
            if (Input.GetKeyUp(KeyCode.RightArrow) && Input.GetKeyUp(KeyCode.LeftArrow))
            {
                particulas.SetActive(false);
            }
            */
        }
        else{
            
            Debug.Log("");
        }
        // Guardado de monedas
        PlayerPrefs.SetInt("run_coins", coins);

    }

    /**void OnCollisionStay2D(Collision2D other) {

        if (other.gameObject.CompareTag("Pasto")){
            velocidad = 0.01f;
        }

        if (other.gameObject.CompareTag("Petroleo")){
            velocidad = 0.001f;
        }

        if(other.gameObject.CompareTag("Hielo")){
            velocidad = 0.02f;
        }
    }**/
    
    void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.CompareTag("coin")) {
            Destroy(other.gameObject);
            audiosource.clip = coinSound;
            audiosource.Play();
            coins = coins + 1*multiplicador;
            var HudManager = FindObjectOfType<hudManager>();
            HudManager.AddMoney(coins);
        }
        if (other.gameObject.CompareTag("Escudo")) {
            Destroy(other.gameObject);

            countShield = 3;
            audiosource.clip = shieldSound;
            audiosource.Play();
            escudo.shieldColor(true);
        }
        //////////////////////////////////////////////////
        if (other.gameObject.CompareTag("CoinsX2")) {
            Destroy(other.gameObject);

            audiosource.clip = xCoins;
            audiosource.Play();
            multiplicador = 2;
            multiplicadorUI.showMultiplier("X2");
            StartCoroutine(coinMultiplier(4, "X2"));
        }
        if (other.gameObject.CompareTag("CoinsX3")) {
            Destroy(other.gameObject);

            audiosource.clip = xCoins;
            audiosource.Play();
            multiplicador = 3;
            multiplicadorUI.showMultiplier("X3");
            StartCoroutine(coinMultiplier(4, "X3"));
        }
        ////////////////////////////////////////////////////
        if (other.gameObject.CompareTag("BotasMas")) {
            Destroy(other.gameObject);

            audiosource.clip = fastBoots;
            audiosource.Play();
            velocidad = 0.15f;
            StartCoroutine(Botas(4));
        }
        if (other.gameObject.CompareTag("Reloj")) {
            Destroy(other.gameObject);

            audiosource.clip = time;
            audiosource.Play();
            Time.timeScale = 0.5f;
            StartCoroutine(slowTime(3));
        }
        if (other.gameObject.CompareTag("BotasMenos")) {
            Destroy(other.gameObject);

            audiosource.clip = slowBoots;
            audiosource.Play();
            velocidad = 0.05f;
            StartCoroutine(Botas(4));
        }
        if (other.gameObject.CompareTag("Enemy")) {
            if (Input.GetKey(KeyCode.UpArrow)){
                audiosource.clip = dodgeSound;
                audiosource.Play();
            }
            else if (countShield > 0) {
                countShield--;
                audiosource.clip = dodgeSound;
                audiosource.Play();
                if (countShield == 0) {
                    audiosource.pitch = 2f;
                    audiosource.clip = shieldSound;
                    audiosource.Play();
                    audiosource.pitch = 1f;
                    escudo.shieldColor(false);
                }

            }
            // danio recibido por alien al jugador 
            // else{
            
            //     audiosource.clip = damageSound;
            //     audiosource.Play();
            //     //velocidad = velocidad*0.01f;
            //     spriterenderer.color = new Color (1, 0, 0, 1); 

            // }
        }
        if (other.gameObject.CompareTag("Disparo")) {
            Destroy(other.gameObject);
            audiosource.clip = Ralentizacion;
            audiosource.Play();

            if (Input.GetKey(KeyCode.UpArrow)){
                audiosource.clip = dodgeSound;
                audiosource.Play();
            }
            else if (countShield > 0) {
                countShield--;
                audiosource.clip = dodgeSound;
                audiosource.Play();
                if (countShield == 0) {
                    audiosource.pitch = 2f;
                    audiosource.clip = shieldSound;
                    audiosource.Play();
                    audiosource.pitch = 1f;
                    escudo.shieldColor(false);
                }
            }

            else{

                velocidad = velocidad*0.5f;
                spriterenderer.color = new Color(0.0f, 0.6f, 0.8f, 1f);
                Debug.Log("dispro");

            }


        }
    }
    void OnTriggerExit2D(Collider2D other){

        spriterenderer.color = new Color (1, 1, 1, 1); 
        velocidad = 0.1f;
    }
    IEnumerator Botas(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        velocidad = 0.1f;
    }
    IEnumerator slowTime(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        Time.timeScale = 1f;
    }
    IEnumerator coinMultiplier(int segundos, string multiplier)
    {
        yield return new WaitForSeconds(segundos);
        multiplicadorUI.hideMultiplier();
        multiplicador = 1;
    }

    
}