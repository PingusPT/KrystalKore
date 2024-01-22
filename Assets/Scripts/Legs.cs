using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    


    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.tag == "Legs")
        {
            collision.gameObject.GetComponent<PlayerMovement>().CatchLegs();
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Player" && gameObject.tag == "BracoVermelho")
        {
            collision.gameObject.GetComponentInChildren<ColorAura>().CatchRedArm();
            //collision.gameObject.GetComponentInChildren<Aura>().CatchRedArm();
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Player" && gameObject.tag == "BracoRoxo")
        {
            collision.gameObject.GetComponentInChildren<ColorAura>().CatchPuprlePower();
            //collision.gameObject.GetComponentInChildren<PlayerMovement>().CatchPurpleArm();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player" && gameObject.tag == "ObjectEnd")
        {
            GameManagerScript.instance.DoEndAnimation();
        }
    }


}
