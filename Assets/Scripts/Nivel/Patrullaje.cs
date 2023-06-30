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
    public float fieldOfViewAngle;

    // Start is called before the first frame update
    void Awake()
    {
      m_rig=GetComponent<Rigidbody2D>();
        

    }

    // Update is called once per frame
    void Update()
    {
      m_rig.velocity = new Vector2(speed , m_rig.velocity.y);  
    }

    private void OnTriggerExit2D(Collider2D other){

      if(other.gameObject.tag == "Pasto")
      {
        speed *= -1;

        childTransform.localPosition = new Vector3(-childTransform.localPosition.x, childTransform.localPosition.y, 0);

      }

      if (other.gameObject.CompareTag("Player"))
      {
        // Dejar de atacar al jugador
        animator.SetBool("Attack", false);
     }

    }

    //float distanceX = transform.position.x - other.transform.position.x;
    //float distanceY = transform.position.y - other.transform.position.y;


    private void OnTriggerEnter2D(Collider2D other){

      float distanceX = transform.position.x - other.transform.position.x;
      float distanceY = transform.position.y - other.transform.position.y;

     if (other.gameObject.CompareTag("Player"))
     {
        // Atacar al jugador
        animator.SetBool("Attack", true);
        var playerTransform = other.gameObject.transform;

        if (distanceX > fieldOfViewAngle && distanceY < 1)
        {
          if (transform.position.x > other.transform.position.x)
          {
            StartCoroutine(test(other, 250));
          }
        }else
          {
            StartCoroutine(test(other, -250));
          }
      }
    }

    private IEnumerator test(Collider2D other, int value){
      yield return new WaitForSeconds(0.08f);
      other.attachedRigidbody.AddForce(Vector2.left*value);
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
