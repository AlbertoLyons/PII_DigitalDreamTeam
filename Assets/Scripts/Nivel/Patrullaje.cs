using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullaje : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Rigidbody2D m_rig;
    public GameObject player;
    private float speed = 2f;
    public Transform childTransform;

    // Start is called before the first frame update
    void Awake()
    {
      m_rig = GetComponent<Rigidbody2D>();
      animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

      m_rig.velocity = new Vector2(speed, m_rig.velocity.y);

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Pasto")
        {
            speed *= -1;
            childTransform.localPosition = new Vector3(-childTransform.localPosition.x, childTransform.localPosition.y, 0);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            // Dejar de atacar al jugador
            //animator.SetBool("Attack", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        float distanceX = transform.position.x - other.transform.position.x;
        float distanceY = transform.position.y - other.transform.position.y;

        if (other.gameObject.CompareTag("Player"))
        {
            // Atacar al jugador
            //animator.SetBool("Attack", true);
            

            if (distanceY < 1)
            {
              Vector2 pushDirection = Vector2.left;
              float pushForce = 10f; // Fuerza de empuje

              if (distanceX > 0)
              {
                PushObject(other.attachedRigidbody, pushDirection, pushForce);
              }
              else
              {
                PushObject(other.attachedRigidbody, -pushDirection, pushForce);
              }
            }
        }
    }

    private void PushObject(Rigidbody2D rb, Vector2 direction, float force)
    {
        rb.velocity = Vector2.zero; // Reiniciar la velocidad del objeto
        rb.AddForce(direction * force, ForceMode2D.Impulse); // Aplicar el impulso de empuje
    }

}
