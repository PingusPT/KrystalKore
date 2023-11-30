using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalAzul : MonoBehaviour
{

    
    Animator anim;
    public bool Invertido = false;

    bool ativador = false;

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

    


    public void Srink()
    {
        if(!ativador)
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
        

    }

    public void Grow()
    {
        if(!ativador)
        {
            if (!Invertido)
            {
                anim.SetFloat("Speed", 1);
            }
            else
            {
                anim.SetFloat("Speed", -1);
            }
        }
        
        
    }

    public void StopAnimation()
    {
        anim.SetFloat("Speed", 0);

    }

    

    public void ModoAtivador()
    {
        ativador = true;
    }

    public void DesativarAtivador()
    {
        ativador = false;
    }

}
