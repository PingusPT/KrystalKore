using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCrystal : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    bool flag = true;
    bool flag2 = true;
    public bool Invertido = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

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
            }

        }

    }



}
