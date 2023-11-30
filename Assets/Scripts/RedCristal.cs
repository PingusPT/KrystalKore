using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCristal : MonoBehaviour
{
    Vector3 point;

    [SerializeField] GameObject aura;

   

    // Start is called before the first frame update
    void Start()
    {
        point = gameObject.transform.position;
    }

    

    public void Explode()
    {
        aura.GetComponent<RedCristalAura>().AtivarBomb();

        RedCristalSpawner.instance.GetRedCristal(gameObject);
        
    }

    public void TurnOfff()
    {
        RedCristalSpawner.instance.isDestroid();
        gameObject.SetActive(false);
       
        
    }
}
