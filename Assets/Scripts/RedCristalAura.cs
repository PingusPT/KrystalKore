using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCristalAura : MonoBehaviour
{

    [SerializeField] GameObject redCristal;

    bool Ativar = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (Ativar)
        {
           
            if (collision.gameObject.tag == "CoisasDestrutiveis")
            {
                
                Destroy(collision.gameObject);
                redCristal.GetComponent<RedCristal>().TurnOfff();
                Invoke("TurnOff", 0.1f);
            }
            else
            {

                redCristal.GetComponent<RedCristal>().TurnOfff();
                Invoke("TurnOff", 0.1f);

            }
        }
        

    }

    public void AtivarBomb()
    {
        Ativar = true;
    }

    public void TurnOff()
    {
        Ativar = false;
    }
}
