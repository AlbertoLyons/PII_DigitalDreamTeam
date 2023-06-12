using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriterenderer;
    [SerializeField] private float velocidad = 0.1f;
    [SerializeField] private int coins = 0;
    [SerializeField] private int HP = 6;
    [SerializeField] private int countShield = 3;
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

    // Start is called before the first frame update
    void Start(){
        //coinSound = GetComponent<>
        spriterenderer = GetComponent<SpriteRenderer>();

        ObtenerPosicionY();
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

            var object_UI = FindObjectOfType<Object_UI>();

            object_UI.shieldColor(true);
        }
        //////////////////////////////////////////////////
        if (other.gameObject.CompareTag("CoinsX2")) {
            Destroy(other.gameObject);

            audiosource.clip = xCoins;
            audiosource.Play();
            multiplicador = 2;
            var object_UI = FindObjectOfType<Object_UI>();

            object_UI.showMultiplier("X2");
            StartCoroutine(coinMultiplier(4, "X2"));
        }
        if (other.gameObject.CompareTag("CoinsX3")) {
            Destroy(other.gameObject);

            audiosource.clip = xCoins;
            audiosource.Play();
            multiplicador = 3;
            var object_UI = FindObjectOfType<Object_UI>();

            object_UI.showMultiplier("X3");
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
                Debug.Log(countShield);
                audiosource.clip = dodgeSound;
                audiosource.Play();
                if (countShield == 0) {
                    var object_UI = FindObjectOfType<Object_UI>();
                    audiosource.pitch = 2f;
                    audiosource.clip = shieldSound;
                    audiosource.Play();
                    audiosource.pitch = 1f;
                    object_UI.shieldColor(false);
                }

            }
            else{
            
                audiosource.clip = damageSound;
                audiosource.Play();
                //velocidad = velocidad*0.01f;
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
        var object_UI = FindObjectOfType<Object_UI>();
        object_UI.hideMultiplier();
        multiplicador = 1;
    }

    public void ObtenerPosicionY(){

        while(true){
            Debug.Log(transform.position.y);
        }
        
    }
}