using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] AudioClip Step1, Step2, Drag, Water1, Water2, Water3, Landing;

    static public PlayerSounds instance;

    bool step = false;
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
}
