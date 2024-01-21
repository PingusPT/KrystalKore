using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoldanaScript : MonoBehaviour
{
    [SerializeField] GameObject CristalRoldana;
    Animator anim;
    Rigidbody2D rgd;
    public float speed = 5f;
    float dir = 0;


    // Start is called before the first frame update
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();

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
}
