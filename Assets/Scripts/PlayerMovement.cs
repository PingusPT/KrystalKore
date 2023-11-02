using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

   Rigidbody2D rgd;

    float horizontal;
    bool ground;
    [SerializeField] float speed = 10f;
    [SerializeField] float JumpForce = 600;

    // Start is called before the first frame update
    void Start()
    {
        rgd = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetButtonDown("Jump") && ground)
        {
            rgd.AddForce(transform.up * JumpForce);
            ground = false;
        }
        if(Input.GetKey(KeyCode.H))
        {
            
            CristalControler.instance.myDelegateGrow();
            
        }
        else if (Input.GetKey(KeyCode.G))
        {
            CristalControler.instance.Shrink();
            
        }
        else
        {
            CristalControler.instance.DelegateStop();
        }



    }
    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(speed * horizontal, 0);
        movement *= Time.deltaTime;

        transform.Translate(movement);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            ground = true;
        }
        
    }
}
