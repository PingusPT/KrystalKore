using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
    [SerializeField] GameObject Luz;

    ColorAura script;

    bool isPlayerCanGrow = true;
    bool hasRedArm = false;
    // Start is called before the first frame update
    void Start()
    {
        script = Luz.GetComponent<ColorAura>();
    }

    // Update is called once per frame
   

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CristalRoxo" && isPlayerCanGrow)
        {

            script.CanGrowPurple();
        }
        if (collision.gameObject.tag == "CristalAzul" || collision.gameObject.tag == "CristalAzulElevador" || collision.gameObject.tag == "CristalAzulRoldana")
        {
            script.CanGrow();
        }
        if(collision.gameObject.tag == "CristalVermelho" && Input.GetKeyDown(KeyCode.V) && hasRedArm)
        {
            //script.ChangeColorToRed();
            collision.gameObject.GetComponent<Animator>().SetTrigger("Ativar");
        }

    }

    
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CristalRoxo")
        {
            script.CantGrowPurple();
        }
        if (collision.gameObject.tag == "CristalAzul" || collision.gameObject.tag == "CristalAzulElevador" || collision.gameObject.tag == "CristalAzulRoldana")
        {
            script.CantGrow();
        }
    }

    public void CatchRedArm()
    {
        hasRedArm = true;
    }
}
