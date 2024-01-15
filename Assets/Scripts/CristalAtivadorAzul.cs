using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalAtivadorAzul : MonoBehaviour
{

    [SerializeField] GameObject Barreira;

    bool flag = true;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<CristalAzul>().ModoAtivador();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(!Barreira.activeSelf && flag)
        {
            flag = false;
            gameObject.GetComponent<CristalAzul>().DesativarAtivador();
            ConveyorSpikeController.instance.AticarTereciro();
        }
    }


   
}
