using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCristal : MonoBehaviour
{
    Vector3 point;

    Animator anim;

    [SerializeField] GameObject aura;

    RedCristalAura auraScript;

    

    // Start is called before the first frame update
    void Start()
    {
        auraScript = aura.GetComponent<RedCristalAura>();

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


    public void Explode()
    {
        auraScript.AtivarBomb();

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
