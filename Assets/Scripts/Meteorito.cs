using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorito : MonoBehaviour
{
    [SerializeField] private float rotacion = 0.1f;
    [SerializeField] private float velocidad = 0.001f;
    [SerializeField] private AudioSource audiosource;
    // Update is called once per frame
    void Update()
    {

         transform.Rotate(0,0,rotacion,Space.Self);
         transform.Translate(0,-velocidad,0,Space.World);
         

    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(other.gameObject);
            audiosource.Play();

            
        }
    }
}
