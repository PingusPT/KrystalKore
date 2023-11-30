using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
    [SerializeField] GameObject Player;
    PlayerMovement script;

    bool isPlayerCanGrow = true;
    bool hasRedArm = false;
    // Start is called before the first frame update
    void Start()
    {
        script = Player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
   

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CristalRoxo" && isPlayerCanGrow)
        {

            script.AutorizarRoxo();
        }
        if (collision.gameObject.tag == "CristalAzul")
        {
            script.AutorizarAzul();
        }
        if(collision.gameObject.tag == "CristalVermelho" && Input.GetKeyDown(KeyCode.V) && hasRedArm)
        {
            script.ChangeColorToRed();
            collision.gameObject.GetComponent<Animator>().SetTrigger("Ativar");
        }

    }

    
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CristalRoxo")
        {
            script.DesautorizarRoxo();
        }
        if (collision.gameObject.tag == "CristalAzul")
        {
            script.DesauturizarAzul();
        }
    }

    public void CatchRedArm()
    {
        hasRedArm = true;
    }
}
