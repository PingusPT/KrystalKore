using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalAzul : MonoBehaviour
{

    
    Animator anim;
    public bool Invertido = false;
    float time;
    float AnimationTimePlyaed;


    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        if(Invertido)
        {
            anim.Play("TesteCristal", 0, 1f);
            anim.SetFloat("Speed", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Srink()
    {

        if (!Invertido)
        {
            anim.SetFloat("Speed", -1);
        }
        else
        {
            anim.SetFloat("Speed", 1);
        }

    }

    public void Grow()
    {
        if(!Invertido)
        {
            anim.SetFloat("Speed", 1);
        }
        else
        {
            anim.SetFloat("Speed", -1);
        }
        
    }

    public void StopAnimation()
    {
        anim.SetFloat("Speed", 0);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Entrou");
        }
    }
}
