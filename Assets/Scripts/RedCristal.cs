using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCristal : MonoBehaviour
{
    Vector3 point;

    Animator anim;

    [SerializeField] GameObject aura;

    float RespawnTime = 6f;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        point = gameObject.transform.localPosition;
    }

    private void Update()
    {
        /*
        if(!gameObject.activeSelf)
        {
            RespawnTime -= Time.deltaTime;

            if(RespawnTime < 0)
            {
                gameObject.SetActive(true);
                anim.Play("Explode", 0, 0);
                anim.SetTrigger("Desativar");
                RespawnTime = 6f;
            }
        }
        */
    }

    public void Explode()
    {
        aura.GetComponent<RedCristalAura>().AtivarBomb();

    }

    public void TurnOfff()
    {
        RedCristalSpawner.instance.StartRespawnTime(gameObject,point,anim);
        gameObject.SetActive(false);
       
        
    }
}
