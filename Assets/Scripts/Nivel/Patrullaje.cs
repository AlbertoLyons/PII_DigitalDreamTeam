using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullaje : MonoBehaviour
{
    private Rigidbody2D m_rig;
    private float speed = 2f;
    public Transform childTransform;
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

      if(other.gameObject.tag == "Pasto"){
        speed *= -1;
        
        childTransform.localPosition = new Vector3(-childTransform.localPosition.x, childTransform.localPosition.y, 0);
        
        
      }  
    }
}
