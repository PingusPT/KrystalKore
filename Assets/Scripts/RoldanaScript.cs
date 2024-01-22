using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoldanaScript : MonoBehaviour
{
    [SerializeField] GameObject CristalRoldana;
    [SerializeField] GameObject Platform;

    Rigidbody2D rgbPlat;
    Animator anim;
    Rigidbody2D rgd;
    public float speed = 5f;
    float dir = 0;


    // Start is called before the first frame update
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        rgbPlat = Platform.GetComponent<Rigidbody2D>();
        anim = CristalRoldana.GetComponent<Animator>();
    }

    // Update is called once per frame
    

    private void FixedUpdate()
    {
        dir = anim.GetFloat("Speed");

        if (dir == 0)
        {
           rgd.velocity = Vector2.zero;
        }
        else if (dir > 0)
        {
           rgd.velocity = transform.right * -speed;
        }
        else if (dir < 0)
        {
           rgd.velocity = transform.right * speed;
        }
    

    }

    //public float divisionVel = 1.7f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            
            //Debug.Log("Empurrar " + rgbPlat.velocity);
            //GameManagerScript.instance.moveScript.InPlataform(new Vector2(Platform.transform.position.x, Platform.transform.position.y + 1f));
            GameManagerScript.instance.moveScript.InPlataform(new Vector2(rgbPlat.velocity.x * 10, 0));
        }
    }
}
