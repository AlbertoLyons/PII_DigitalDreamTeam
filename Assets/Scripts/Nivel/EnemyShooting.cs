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

    public float rango_vision;

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

        bool isFlipped = GetComponent<SpriteRenderer>().flipX;

        float distanceX = Math.Abs(transform.position.x - player.transform.position.x);
        float distanceY = Math.Abs(transform.position.y - player.transform.position.y);

        if (distanceX < 12 && distanceY < 1)
        {
            animator.SetBool("disparo", true);
        }
        else
        {
            animator.SetBool("disparo", false);

        }

        if (distanceX > rango_vision && distanceY < 1)
        {
            if (transform.position.x > player.transform.position.x)
            {
                if (isFlipped)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
            else
            {
                if (isFlipped)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
    }

    void shoot()
    {
        Instantiate(proyectil, proyectilPos.position, Quaternion.identity);
        
    }


    public void ShootAnimationFinished()
    {
        shoot();
    }
}
