using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalRoxo : MonoBehaviour
{

    public bool invertido = false;

    // Start is called before the first frame update
    void Start()
    {
        if(invertido)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Appear()
    {
       
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    
}
