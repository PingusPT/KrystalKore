using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCristal : MonoBehaviour
{
    Vector3 point;

    Animator anim;

    [SerializeField] GameObject aura;

    

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        point = gameObject.transform.localPosition;
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

    public void StartExplo()
    {
        anim.SetTrigger("Ativar");
    }
}
