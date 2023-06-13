using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyShooting : MonoBehaviour
{
    public GameObject proyectil;
    public Transform proyectilPos;
    private Animator animator;

    private float timer;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float distanceX = Math.Abs(transform.position.x - player.transform.position.x);

        float distanceY = Math.Abs(transform.position.y - player.transform.position.y);

        Debug.Log(distanceX);


        if (distanceX < 9 && distanceY < 1)
        {
            if (timer > 2f)
            {
                timer = 0;
                shoot();
            }
        }
        else
        {
            animator.SetBool("disparo", false);
        }
    }

    void shoot()
    {
        Instantiate(proyectil, proyectilPos.position, Quaternion.identity);
        animator.SetBool("disparo", true);
    }
}