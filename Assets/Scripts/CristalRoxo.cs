using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalRoxo : MonoBehaviour
{
    Rigidbody2D rb;

    Animator anim;

    BoxCollider2D collider2d;

    public bool invertido = false;

    public float MassSet = 60;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.layer == 9)
        {
            rb = gameObject.GetComponent<Rigidbody2D>();


            if (!invertido)
            {
                rb.mass = MassSet;
            }



        }
        else
        {
            collider2d = gameObject.GetComponent<BoxCollider2D>();

            if(invertido)
            {
                collider2d.enabled = false;
            }
            
        }
        anim = gameObject.GetComponent<Animator>();

        if (invertido)
        {
            anim.Play("aparecer", 0, 1f);
            anim.SetFloat("speed", 1);
            

        }
        else
        {
            anim.Play("aparecer", 0, 0f);
            anim.SetFloat("speed", -1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0)
        {
            anim.Play("aparecer", 0, 0f);
        }
        else if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            anim.Play("aparecer", 0, 1f);
        }

    }

    public void Appear()
    {
       
        if(anim.GetFloat("speed") > 0)
        {
            anim.SetFloat("speed", -1);
        }
        else
        {
            anim.SetFloat("speed", 1);
        }
    }

    public void TurnOffCollider()
    {
        if(anim.GetFloat("speed") > 0)
        {
           if(gameObject.layer == 9)
            {
                rb.mass = 1;
            }
           else
            {
                collider2d.enabled = false;
            }
            
        }
        else
        {
            if (gameObject.layer == 9)
            {
                rb.mass = MassSet;
            }
            else
            {
                collider2d.enabled = true;
            }
            
        }
        
    }
    
}
