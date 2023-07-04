using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class AtaqueAnim : MonoBehaviour
{
    private Animator animator;
    public GameObject player;
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
        float distanceX = Math.Abs(transform.position.x - player.transform.position.x);
        float distanceY = Math.Abs(transform.position.y - player.transform.position.y);
        
        if (distanceX < 1.5f && distanceY < 1)
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
        
    }
}
