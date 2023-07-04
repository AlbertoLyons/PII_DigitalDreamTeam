using UnityEngine;

public class Ftienda : MonoBehaviour
{
    private static Ftienda instance;

    public static Ftienda Instance
    {
        get { return instance; }
    }

    public float tiempoEspera = 2f; // Tiempo de espera en segundos

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
