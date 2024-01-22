using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class DetectionDoor : MonoBehaviour
{

    [SerializeField] GameObject Porta;
    
    Animator anim;

    bool flag = true;
    bool flag1 = true;


    float time = 4f;
    bool CountDown = false;

    // Start is called before the first frame update
    void Start()
    {
        
        anim = Porta.GetComponent<Animator>();
 
    }

    // Update is called once per frame
    void Update()
    {
       
        if(CountDown)
        {
            
            time -= Time.deltaTime;

            
            if(time < 0)
            {
                GameManagerScript.instance.moveScript.enabled = true;
                CountDown = false;
                time = 4f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && flag)
        {
            
            flag = false;
            
            anim.SetTrigger("fechar");
            collision.gameObject.GetComponent<Animator>().SetBool("Idle", true);
            GameManagerScript.instance.moveScript.enabled = false;
            CountDown = true;
        }
        if(collision.gameObject.tag == "Chave" && flag1)
        {
            
            
            anim.SetTrigger("abrir");


            Destroy(collision.gameObject);
        }
    }

}
