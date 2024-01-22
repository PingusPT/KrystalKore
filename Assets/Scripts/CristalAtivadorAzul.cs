using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalAtivadorAzul : CristalAzul
{

    [SerializeField] GameObject Barreira;

    

    bool flag = true;

   
    // Update is called once per frame
    void Update()
    {
        if(!Barreira.activeSelf && flag && Barreira.name != "Break2")
        {
            flag = false;
           
            ConveyorSpikeController.instance.AticarTereciro();
        }

        base.FixAnimation();
    }

    override public void Grow()
    {
        if(!flag)
        {
            base.Grow();
        }
        
    }

    public override void Srink()
    {
        if(!flag)
        {
            base.Srink();
        }
        
    }

    public override void StopAnimation()
    {
        base.StopAnimation();
    }
}
