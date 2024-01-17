using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCrystal : CristalAzul
{
    // Start is called before the first frame update
    
    bool flag = true;
    bool flag2 = true;

    // Update is called once per frame
    void Update()
    {
        
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && flag)
        {
            flag = false;
            flag2 = true;
            if(Invertido)
            {
                ConveyorSpikeController.instance.ChangeDir2();
            }
            else
            {
                ConveyorSpikeController.instance.ChangeDir1();
                ConveyorSpikeController.instance.ChangeDir3();
            }
            
            
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9)
        {
           flag = true;

        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f && flag2)
        {
           flag2 = false;
            if(Invertido)
            {
                ConveyorSpikeController.instance.ChangeDir2();
            }
            else
            {
                ConveyorSpikeController.instance.ChangeDir1();
                ConveyorSpikeController.instance.ChangeDir3();
            }

        }

        base.FixAnimation();

    }



}
