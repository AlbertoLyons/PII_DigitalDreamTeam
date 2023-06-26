using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriterenderer;
    
    [SerializeField] public static float velocidad;
    [SerializeField] public static int coins = 0;
    [SerializeField] private bool isBotasMas;
    [SerializeField] private bool isBotasMenos;
    [SerializeField] private bool isRalentizado = false;
    [SerializeField] private bool isPowerPellet;
    [SerializeField] private GameObject particulasMuerteEnemigo;

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
    [SerializeField] private AudioClip powerPellet;
    [SerializeField] private AudioClip enemyDeath;

    [SerializeField] private Shield_UI escudo;
    [SerializeField] private Power_Pellet_UI powerPelletUI;
    [SerializeField] private Multiplier_UI multiplicadorUI;
    //[SerializeField] private GameObject particulas;


    // Start is called before the first frame update
    void Start(){
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
        else{ Debug.Log(""); }
    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("coin")) 
        {
            other.gameObject.SetActive(false);
            audiosource.clip = coinSound;
            audiosource.Play();
            coins = coins + 1*multiplicador;
            var HudManager = FindObjectOfType<hudManager>();
            HudManager.AddMoney(coins);
        }
        if (other.gameObject.CompareTag("Escudo")) 
        {
            other.gameObject.SetActive(false);

            countShield = 3;
            audiosource.clip = shieldSound;
            audiosource.Play();
            escudo.shieldColor(true);
        }
        if (other.gameObject.CompareTag("PowerPellet")) {
            Destroy(other.gameObject);
            audiosource.clip = powerPellet;
            audiosource.Play();
            isPowerPellet = true;
            powerPelletUI.PowerColor(isPowerPellet);
            StartCoroutine(PowerPellet(4));
        }
        //////////////////////////////////////////////////
        if (other.gameObject.CompareTag("CoinsX2")) 
        {
            other.gameObject.SetActive(false);

            audiosource.clip = xCoins;
            audiosource.Play();
            multiplicador = 2;
            multiplicadorUI.showMultiplier("X2");
            StartCoroutine(coinMultiplier(4, "X2"));
        }
        if (other.gameObject.CompareTag("CoinsX3")) 
        {
            other.gameObject.SetActive(false);

            audiosource.clip = xCoins;
            audiosource.Play();
            multiplicador = 3;
            multiplicadorUI.showMultiplier("X3");
            StartCoroutine(coinMultiplier(4, "X3"));
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
            Meteorito.velocidad = 0.02f;
            // slowScreen = GameObject.FindGameObjectWithTag("Slowmo");
            //GameObject.FindGameObjectWithTag("Slowmo").SetActive(true);
            StartCoroutine(slowTime(3));
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
                audiosource.clip = enemyDeath;
                audiosource.Play();
                particulasMuerteEnemigo.transform.position = new Vector3(other.gameObject.transform.position.x,other.gameObject.transform.position.y,0);
                particulasMuerteEnemigo.SetActive(true);
                StartCoroutine(EnemyParticleDeath(1));
            }


            if (Input.GetKey(KeyCode.UpArrow))
            {
                audiosource.clip = dodgeSound;
                audiosource.Play();
            }
            else if (countShield > 0) 
            {
                countShield--;
                audiosource.clip = dodgeSound;
                audiosource.Play();
                if (countShield == 0) 
                {
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
                if (!isRalentizado)
                {
                    audiosource.clip = Ralentizacion;
                    audiosource.Play();
                    velocidad = VelocidadDeMovimiento() * 0.5f;
                    spriterenderer.color = new Color(0.0f, 0.6f, 0.8f, 1f);
                    StartCoroutine(VelocidadConRalentizacion(1.5f));
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
        //slowScreen = GameObject.FindGameObjectWithTag("Slowmo");
        //GameObject.FindGameObjectWithTag("Slowmo").SetActive(false);
        Meteorito.velocidad = 0.075f;
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
}