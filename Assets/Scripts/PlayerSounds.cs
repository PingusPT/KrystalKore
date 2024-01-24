using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] AudioClip Step1, Step2, Drag, Water1, Water2, Water3, Landing, Die1, Die2, Damage1, Damage2, Damage3, Damage4, Damage5, Damage6;

    static public PlayerSounds instance;

    bool step = false;
    bool dieSound = false;
    bool PlayerOnWater = false;
    

    AudioSource src;
    void Start()
    {
        instance = this;

        src = gameObject.GetComponent<AudioSource>();

        src.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StepSoud()
    {
        if(step)
        {
            if (!PlayerOnWater)
            {
                src.PlayOneShot(Step1);
            }
                
        }
        else
        {
            if (!PlayerOnWater)
            {
                src.PlayOneShot(Step2);
            }
        }

        step = !step;
    }

    private void DragSound()
    {
        src.PlayOneShot(Drag);
    }

    

    public void WaterSounds()
    {
        switch (Random.Range(1, 4))
        {
            case 1:
                {
                    
                    src.PlayOneShot(Water1);
                    break;
                }
            case 2:
                {
                    
                    src.PlayOneShot(Water2);
                    break;
                }
            case 3:
                {
                    
                    src.PlayOneShot(Water3);
                    break;
                }
        }
    }

    public void LandingSound()
    {
        src.PlayOneShot(Landing);
    }

    private void DieSound()
    {
        if(dieSound)
        {
            
            src.PlayOneShot(Die1);
        }
        else
        {
            
            src.PlayOneShot(Die2);
        }

        dieSound = !dieSound;
    }

    private void TakeDamageSound()
    {
        switch (Random.Range(1, 7))
        {
            case 1:
                {

                    src.PlayOneShot(Damage1);
                    break;
                }
            case 2:
                {

                    src.PlayOneShot(Damage2);
                    break;
                }
            case 3:
                {

                    src.PlayOneShot(Damage3);
                    break;
                }
            case 4:
                {
                    src.PlayOneShot(Damage4);
                    break;
                }
            case 5:
                {
                    src.PlayOneShot(Damage5);
                    break;
                }
            case 6:
                {
                    src.PlayOneShot(Damage6);
                    break;
                }
        }
    }
}
