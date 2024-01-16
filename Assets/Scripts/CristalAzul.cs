using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalAzul : MonoBehaviour
{

    
    Animator anim;
    public bool Invertido = false;
  
    bool ativador = false;
   
    AnimatorStateInfo info;
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
    

    private void Update()
    {
        info = anim.GetCurrentAnimatorStateInfo(0);
        
        

        if (info.normalizedTime > 1)
        {
            if (gameObject.tag == "CristalAzul")
            {

                anim.Play("TesteCristal", 0, 1f);
            }
            else if (gameObject.tag == "CristalAzulElevador")
            {

                anim.Play("CristalElevador", 0, 1f);
            }
            else if (gameObject.tag == "CristalAzulRoldana")
            {

                anim.Play("CristalRoldana", 0, 1f);
            }



        }

        if (info.normalizedTime < 0)
        {
            if (gameObject.tag == "CristalAzul")
            {

                anim.Play("TesteCristal", 0, 0f);
            }
            else if (gameObject.tag == "CristalAzulElevador")
            {

                anim.Play("CristalElevador", 0, 0f);
            }
            else if (gameObject.tag == "CristalAzulRoldana")
            {

                anim.Play("CristalRoldana", 0, 0f);
            }
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

        if(anim != null)
        {
            anim.SetFloat("Speed", 0);
        }
        

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
