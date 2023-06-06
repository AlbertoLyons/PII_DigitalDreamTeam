using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorNivel : MonoBehaviour
{
    [SerializeField] private GameObject[] partesNivel;
    [SerializeField] private float distanciaMinima;
    [SerializeField] private Transform puntoFinal;
    [SerializeField] private int cantidadInicial;
    private Transform jugador;

    private GameObject nivelActual;

    void Start(){
        jugador = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < cantidadInicial; i++){
            GenerarParteNivel();
        }
    }

    void Update() {
        float jugadorY = jugador.position.y;
        float puntoY = puntoFinal.position.y;

        if (Mathf.Abs(jugadorY - puntoY) < distanciaMinima){
            for (int i = 0; i < 3; i++){
                GenerarParteNivel();

            }
        }
    }

    void GenerarParteNivel(){
        Vector3 puntoY = new Vector3(-12.06f, puntoFinal.position.y - 0.843264f);
        int numeroAleatorio = Random.Range(0, partesNivel.Length);
        nivelActual = Instantiate(partesNivel[numeroAleatorio], puntoY, Quaternion.identity);
        puntoFinal = BuscarPuntoFinal(nivelActual, "PuntoFinal");

        Camera.main.Render();

        StartCoroutine(DestruirNivelDespuesDeDelay(nivelActual, 15f));
    }

    Transform BuscarPuntoFinal(GameObject parteNivel, string etiqueta){
        Transform punto = null;
        foreach (Transform ubicacion in parteNivel.transform){
            if (ubicacion.CompareTag(etiqueta)){
                punto = ubicacion;
                break;
            }
        }
        return punto;
    }

    IEnumerator DestruirNivelDespuesDeDelay(GameObject nivel, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(nivel);
    }
}