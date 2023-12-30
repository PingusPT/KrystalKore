using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    [SerializeField] private LayerMask ignorMe; 
    private RaycastHit2D Hit2D;
    [SerializeField] Transform feet;
    Rigidbody2D rgb;
    public float speed = 0.5f;
    public bool invertido = false;
    
    private void Start()
    {
        rgb = feet.GetComponent<Rigidbody2D>();
        float z = gameObject.transform.rotation.z;

        if(invertido)
        {
            speed *= -1;
        }
        
    }
    // Start is called before the first frame update
   
    private void Update()
    {

        rgb.velocity = feet.transform.right * speed;

        GroundCheck();
    }
    

    private void GroundCheck()
    {
        Hit2D = Physics2D.Raycast(transform.position, -feet.transform.up, 2f, ignorMe);
        Debug.DrawRay(transform.position, -feet.transform.up, Color.blue);
        if(Hit2D != false)
        {
            

            Vector2 temp = feet.position;
            temp.y = Hit2D.point.y;
            feet.transform.position = temp;
            

            
            
            feet.transform.up = Hit2D.normal;
            feet.transform.eulerAngles = new Vector3(0, 0, feet.transform.eulerAngles.z);

        }
    }

    public void ChangeDirection()
    {
        speed *= -1;
    }

    public void Stop()
    {
        speed = 0;
    }

    public void Walk()
    {
        speed = 0.5f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<LifeManager>().TakeDamage();
        }
    }


}
