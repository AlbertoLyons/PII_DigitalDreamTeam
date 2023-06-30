using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class Player : MonoBehaviour{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriterenderer;
    
    [SerializeField] public static float velocidad;
    [SerializeField] public static int coins = 0;
    [SerializeField] public static int ScorePoints = 0;
    [SerializeField] private float parryCooldown = 1f;
    private bool canParry = true; 
    [SerializeField] private GameObject particulasMuerteEnemigo;
    [SerializeField] private ParticleSystem particulasDisparo;
    [SerializeField] private ParticleSystem particulasDisparo2;
    [SerializeField] private ParticleSystem particulasDodge;
    [SerializeField] private ParticleSystem lluviaMonedas;
    [SerializeField] private GameObject slowScreen;
    public static Rigidbody2D rb;

    [SerializeField] float fuerza;
    public bool tocandoSuelo;

    //[SerializeField] private int HP = 6;
    [SerializeField] public static int countShield = 0;
    [SerializeField] private int multiplicador = 1;
    [SerializeField] private bool isBotasMas;
    [SerializeField] private bool isBotasMenos;
    [SerializeField] private bool isRalentizado = false;
    [SerializeField] private bool isPowerPellet;

    [SerializeField] private AudioSource audiosource;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip dodgeSound;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip fastBoots;
    [SerializeField] private AudioClip slowBoots;
    [SerializeField] private AudioClip time;
    [SerializeField] private AudioClip Ralentizacion;
    [SerializeField] private AudioClip enemyDeath;
    [SerializeField] private AudioClip lluviaSonido;

    [SerializeField] private Shield_UI escudo;
    [SerializeField] private Power_Pellet_UI powerPelletUI;
    [SerializeField] private Multiplier_UI multiplicadorUI;
    [SerializeField] public Coin_UI coinUI;
    //[SerializeField] private GameObject particulas;


    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        velocidad = VelocidadDeMovimiento();
        countShield = 0;
        //coinSound = GetComponent<>
        spriterenderer = GetComponent<SpriteRenderer>();
        coins = 0;
        
    }

    // Update is called once per frame
    void Update(){
        // Guardado de monedas
        PlayerPrefs.SetInt("run_coins", coins);

        //particulaMovimiento.transform.Translate(transform.position.x,transform.position.y,0);
        if(this != null){
            //Movimiento a la izquierda
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
                spriterenderer.flipX = true;
                //particulas.SetActive(true);
                transform.Translate(-velocidad, 0, 0);
                animator.SetBool("Derecha", true);
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) {
                animator.SetBool("Derecha", false);
                
            }
            
            //Movimiento a la derecha
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
                spriterenderer.flipX = false;
                //particulas.SetActive(true);
                transform.Translate(velocidad, 0, 0);
                animator.SetBool("Derecha", true);
            }
            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) {
                animator.SetBool("Derecha", false);
                
            }

            //Salto
            if (Input.GetKeyDown(KeyCode.Space) && tocandoSuelo == true){

                rb.AddForce(Vector2.up * fuerza, ForceMode2D.Impulse);
                tocandoSuelo = false;
            }

            //Condiciones del parry
            if (Input.GetKey(KeyCode.UpArrow) && canParry) {
                spriterenderer.color = Color.green;
                
            }
            if (Input.GetKeyUp(KeyCode.UpArrow) && canParry) {
                spriterenderer.color = new Color(225,250,223,255);
            }

            /*
            if (Input.GetKeyUp(KeyCode.RightArrow) && Input.GetKeyUp(KeyCode.LeftArrow))
            {
                particulas.SetActive(false);
            }
            */
        }
        else{ Debug.Log(""); }
    }

    void OnParticleCollision(GameObject other)
    {

        if(other.gameObject.CompareTag("coin"))
        {
            coins = coins + 1*multiplicador;
            ScorePoints = coins * 100;
            var HudManager = FindObjectOfType<hudManager>();
            HudManager.AddScore(ScorePoints);
            HudManager.AddMoney(coins);
            coinUI.RecogeMoneda();
        }

    }
    

    void OnTriggerEnter2D(Collider2D other) 
    {
    
        if (other.gameObject.CompareTag("coin")) 
        {
            var randomNumber = Random.Range(1,30);
            other.gameObject.SetActive(false);
            // audiosource.clip = coinSound;
            // audiosource.Play();
            coins = coins + 1*multiplicador;
            ScorePoints = coins * 100;
            var HudManager = FindObjectOfType<hudManager>();
            HudManager.AddMoney(coins);
            HudManager.AddScore(ScorePoints);
            coinUI.RecogeMoneda();
            if(randomNumber == 10){
                lluviaMonedas.Play();
                audiosource.clip = lluviaSonido;
                audiosource.Play();
                StartCoroutine(LluviaMonedas(5));
            }
        }
        if (other.gameObject.CompareTag("Escudo")) 
        {
            other.gameObject.SetActive(false);
            countShield = 3 + PlayerPrefs.GetInt(Manager_Tienda.keyCompras[1]);
            escudo.shieldColor(true);
        }
        if (other.gameObject.CompareTag("PowerPellet")) 
        {
            other.gameObject.SetActive(false);
            isPowerPellet = true;
            powerPelletUI.PowerColor(isPowerPellet);
            StartCoroutine(PowerPellet(4 + 2 * PlayerPrefs.GetInt(Manager_Tienda.keyCompras[3])));
        }
        //////////////////////////////////////////////////
        if (other.gameObject.CompareTag("CoinsX2")) 
        {
            other.gameObject.SetActive(false);

            multiplicador = 2;
            multiplicadorUI.showMultiplier("X2");
            StartCoroutine(coinMultiplier(4 + 5 * PlayerPrefs.GetInt(Manager_Tienda.keyCompras[4]), "X2"));
        }
        if (other.gameObject.CompareTag("CoinsX3")) 
        {
            other.gameObject.SetActive(false);

            multiplicador = 3;
            multiplicadorUI.showMultiplier("X3");
            StartCoroutine(coinMultiplier(4  + 5 * PlayerPrefs.GetInt(Manager_Tienda.keyCompras[5]), "X3"));
        }
        ////////////////////////////////////////////////////
        if (other.gameObject.CompareTag("BotasMas")) 
        {
            other.gameObject.SetActive(false);

            audiosource.clip = fastBoots;
            audiosource.Play();
            isBotasMas = true;
            velocidad = VelocidadConBotasMas();
            StartCoroutine(BotasMas(4));
        }
        if (other.gameObject.CompareTag("Reloj")) 
        {
            other.gameObject.SetActive(false);

            audiosource.clip = time;
            audiosource.Play();
            Time.timeScale = 0.5f;
            rb.gravityScale = 4.9f;
            Meteorito.velocidad = Meteorito.velocidad*0.5f;
            slowScreen.SetActive(true);
            StartCoroutine(slowTime(2 + PlayerPrefs.GetInt(Manager_Tienda.keyCompras[2])));
        }
        if (other.gameObject.CompareTag("BotasMenos")) 
        {
            other.gameObject.SetActive(false);

            audiosource.clip = slowBoots;
            audiosource.Play();
            isBotasMenos = true;
            velocidad = VelocidadConBotasMenos();
            StartCoroutine(BotasMenos(4));
        }
        if (other.gameObject.CompareTag("Enemy")) 
        {
	        if (isPowerPellet) {
                other.gameObject.SetActive(false);
                powerPelletUI.EnemyDeath();
                particulasMuerteEnemigo.transform.position = new Vector3(other.gameObject.transform.position.x,other.gameObject.transform.position.y,0);
                particulasMuerteEnemigo.SetActive(true);
                StartCoroutine(EnemyParticleDeath(1));
            }
            if (Input.GetKey(KeyCode.UpArrow) && canParry || Input.GetKey(KeyCode.Space) && canParry)
            {   
                PerformParry();
            }
            /*
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
            {
                particulasDodge.Play();
                audiosource.clip = dodgeSound;
                audiosource.Play();
            }
            */
            else if (countShield > 0) 
            {
                countShield--;
                particulasDodge.Play();
                audiosource.clip = dodgeSound;
                audiosource.Play();
                if (countShield == 0) 
                {
                    audiosource.pitch = 2f;
                    audiosource.pitch = 1f;
                    escudo.shieldColor(false);
                }
            }
        }
        if (other.gameObject.CompareTag("Disparo")) {
            Destroy(other.gameObject);
            if (Input.GetKey(KeyCode.UpArrow) && canParry || Input.GetKey(KeyCode.Space) && canParry)
            {   
                PerformParry();
            }
            /*
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space)){
                particulasDodge.Play();
                audiosource.clip = dodgeSound;
                audiosource.Play();
            }
            */
            else if (countShield > 0) {
                countShield--;
                particulasDodge.Play();
                audiosource.clip = dodgeSound;
                audiosource.Play();
                if (countShield == 0) {
                    audiosource.pitch = 2f;
                    audiosource.pitch = 1f;
                    escudo.shieldColor(false);
                }
            }
            else{
                if (!isRalentizado)
                {
                    particulasDisparo.Play();
                    audiosource.clip = Ralentizacion;
                    audiosource.Play();
                    velocidad = VelocidadDeMovimiento() * 0.5f;
                    spriterenderer.color = new Color(0.0f, 0.6f, 0.8f, 1f);
                    StartCoroutine(VelocidadConRalentizacion(1.5f));
                }
                else if (isRalentizado)
                {
                    particulasDisparo2.Play();
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
    }
    IEnumerator BotasMas(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        isBotasMas = false;
        velocidad = VelocidadDeMovimiento();
    }
    IEnumerator BotasMenos(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        isBotasMas = false;
        velocidad = VelocidadDeMovimiento();
    }
    IEnumerator slowTime(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        Time.timeScale = 1f;
        slowScreen.SetActive(false);
        Meteorito.velocidad = 0.07f;
        rb.gravityScale = 1.9f;
    }
    IEnumerator coinMultiplier(int segundos, string multiplier)
    {
        yield return new WaitForSeconds(segundos);
        multiplicadorUI.hideMultiplier();
        multiplicador = 1;
    }
    IEnumerator VelocidadConRalentizacion(float segundos)
    {
        if(!isRalentizado)
        {
            isRalentizado = true;
            yield return new WaitForSeconds(segundos);
            velocidad = VelocidadDeMovimiento();
            spriterenderer.color = new Color (1, 1, 1, 1);
            isRalentizado = false;
        }        
    }
    IEnumerator PowerPellet(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        isPowerPellet = false;
        powerPelletUI.PowerColor(isPowerPellet);
    }
    IEnumerator EnemyParticleDeath(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        particulasMuerteEnemigo.SetActive(false);
    }
    IEnumerator LluviaMonedas(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        lluviaMonedas.Stop();
    }
    void PerformParry()
    {
        particulasDodge.Play();
        audiosource.clip = dodgeSound;
        audiosource.Play();
        canParry = false;
        spriterenderer.color = new Color(225,250,223,255);
        Invoke("ResetParry", parryCooldown);

    }
    void ResetParry() {
        canParry = true;
    }
    public float VelocidadDeMovimiento()
    {
        //velocidad base para el jugador
        velocidad = 0.1f;
        //si no tiene la compra de mejora 
        if (PlayerPrefs.GetInt(Manager_Tienda.keyCompras[0]) == 0) { return velocidad; }
        //si tiene la compra de mejora 
        else if (PlayerPrefs.GetInt(Manager_Tienda.keyCompras[0]) > 0)
        {
            double velocidadDouble = velocidad * Math.Pow(1.07, PlayerPrefs.GetInt(Manager_Tienda.keyCompras[0]));
            velocidad = (float)velocidadDouble;
            return velocidad;
        }
        return velocidad;
    }    
    public float VelocidadConBotasMas()
    {
        if (isBotasMas)
        {
            float velocidadConBotasMas = VelocidadDeMovimiento() * 1.6f;
            return velocidadConBotasMas;
        }    
        else if(!isBotasMas)
        {   
            float velocidadSinBotasMas = VelocidadDeMovimiento();
            return velocidadSinBotasMas;
        }
        return VelocidadDeMovimiento();
    }    
    public float VelocidadConBotasMenos()
    {
        if (isBotasMenos)
        {
            float velocidadConBotasMenos = VelocidadDeMovimiento() * 0.5f;
            return velocidadConBotasMenos;
        }    
        else if(!isBotasMenos)
        {
            float velocidadSinBotasMenos = VelocidadDeMovimiento();
            return velocidadSinBotasMenos;
        }
        return VelocidadDeMovimiento(); 
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Pasto")
        {
            tocandoSuelo = true;
        }

    }
}