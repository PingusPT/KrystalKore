using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCristal : MonoBehaviour
{
    Vector3 point;

    Animator anim;

    List<GameObject> arrayDestructibleObjects = new List<GameObject>();

    bool exploded = false;

    
    void Start()
    {
        

        if(anim)
        {
            Debug.Log("Red Cristal");
        }
        else
        {
            Debug.Log("Red Cristal Null");
        }
    }

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        point = gameObject.transform.localPosition;
    }

    public void TurnOfff()
    {
        RedCristalSpawner.instance.StartRespawnTime(gameObject,point,anim);
        gameObject.SetActive(false);
        exploded = false;
    }

    public void StartExplo()
    {
        anim.SetTrigger("Ativar");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "CoisasDestrutiveis" && collision.gameObject.name != "Break1") || collision.gameObject.tag == "EngrenagemDestrutivel" || collision.gameObject.tag == "Player")
        {
            Debug.Log("Obejto entrou  - " + collision.gameObject.name);
            arrayDestructibleObjects.Add(collision.gameObject);

        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "CoisasDestrutiveis" && collision.gameObject.name != "Break1") || collision.gameObject.tag == "EngrenagemDestrutivel" || collision.gameObject.tag == "Player")
        {
            Debug.Log("Obejto saiu  - " + collision.gameObject.name);
            arrayDestructibleObjects.Remove(collision.gameObject);

        }
    }

    private void Explode()
    {
        
        if (!exploded)
        {
            Debug.Log("AAAA * " + gameObject.name + " + position + " + transform.position );
            foreach (GameObject obj in arrayDestructibleObjects.ToArray())
            {
                if (obj.gameObject.tag == "CoisasDestrutiveis" && obj.gameObject.name != "Break1")
                {
                    obj.gameObject.SetActive(false);
                    GameManagerScript.instance.ObjectDestroid(obj.gameObject);

                }
                if (obj.gameObject.tag == "EngrenagemDestrutivel")
                {
                    obj.gameObject.GetComponent<AberturasExplo>().Ativacao();
                }
                else if (obj.gameObject.tag == "Player")
                {
                    GameManagerScript.instance.lifeScript.TakeDamage();
                }
            }


            exploded = true;
        }
    }
}
