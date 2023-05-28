using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Parallax : MonoBehaviour
{
    private float length, startpos;
    public GameObject camara;
    public float parallaxFX;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (camara.transform.position.y *(1 - parallaxFX));
        float distancia = (camara.transform.position.y * parallaxFX);
        
        transform.position = new Vector3(transform.position.x, startpos + distancia, transform.position.z);
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}
