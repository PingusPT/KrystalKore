using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalRoxo : MonoBehaviour
{

    Animator anim;

    BoxCollider2D collider2d;

    public bool invertido = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        collider2d = gameObject.GetComponent<BoxCollider2D>();

        if(invertido)
        {
            anim.Play("aparecer", 0, 1f);
            anim.SetFloat("speed", 1);
            collider2d.enabled = false;
            
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
            Debug.Log("AAAA");
            collider2d.enabled = false;
        }
        else
        {
            Debug.Log("BBBB");
            collider2d.enabled = true;
        }
        
    }
    
}
