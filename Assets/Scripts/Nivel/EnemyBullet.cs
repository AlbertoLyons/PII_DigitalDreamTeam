using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBullet : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float force;
    public float fieldOfViewAngle;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            return;
        }
        else
        {
            Vector3 direction = player.transform.position - transform.position;
            rb.velocity = new Vector2(direction.x, 0).normalized * force;
        }
        bool isFlipped = GetComponent<SpriteRenderer>().flipX;

        float distanceX = Math.Abs(transform.position.x - player.transform.position.x);
        float distanceY = Math.Abs(transform.position.y - player.transform.position.y);

        if (distanceX > fieldOfViewAngle && distanceY < 1)
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

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerInFieldOfView())
        {
            // Disparar al jugador
        }

        bool isFlipped = GetComponent<SpriteRenderer>().flipX;

        float distanceX = Math.Abs(transform.position.x - player.transform.position.x);
        float distanceY = Math.Abs(transform.position.y - player.transform.position.y);

    }

    bool IsPlayerInFieldOfView()
    {
        if (player == null)
        {
            return false;
        }
        else
        {
            Vector3 direction = player.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.right);

            if (angle <= fieldOfViewAngle * 0.5f)
            {
                // El jugador está dentro del campo de visión
                return true;
            }

            // El jugador está fuera del campo de visión
            return false;

        }
        
    }
}
