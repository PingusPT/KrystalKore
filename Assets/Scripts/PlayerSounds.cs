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
                if (src != null && Step1 != null)
                {
                    src.PlayOneShot(Step1);
                }
                else
                {
                    Debug.LogError("erro neste Game Object" + gameObject.name);
                }
            }
                
        }
        else
        {
            if (!PlayerOnWater)
            {
                if (src != null && Step2 != null)
                {
                    src.PlayOneShot(Step2);
                }
                else
                {
                    Debug.LogError("erro neste Game Object" + gameObject.name);
                }
            }
        }

        step = !step;
    }

    private void DragSound()
    {
        if (src != null && Drag != null)
        {
            src.PlayOneShot(Drag);
        }
        else
        {
            Debug.LogError("erro neste Game Object" + gameObject.name);
        }
    }

    

    public void WaterSounds()
    {
        switch (Random.Range(1, 4))
        {
            case 1:
                {
                    if (src != null && Water1 != null)
                    {
                        src.PlayOneShot(Water1);
                    }
                    else
                    {
                        Debug.LogError("erro neste Game Object" + gameObject.name);
                    }
                    break;
                }
            case 2:
                {
                    if (src != null && Water2 != null)
                    {
                        src.PlayOneShot(Water2);
                    }
                    else
                    {
                        Debug.LogError("erro neste Game Object" + gameObject.name);
                    }
                    break;
                }
            case 3:
                {
                    if (src != null && Water3 != null)
                    {
                        src.PlayOneShot(Water3);
                    }
                    else
                    {
                        Debug.LogError("erro neste Game Object" + gameObject.name);
                    }
                    break;
                }
        }
    }

    public void LandingSound()
    {
        if (src != null && Landing != null)
        {
            src.PlayOneShot(Landing);
        }
        else
        {
            Debug.LogError("erro neste Game Object" + gameObject.name);
        }
    }

    private void DieSound()
    {
        if(dieSound)
        {
            if (src != null && Die1 != null)
            {
                src.PlayOneShot(Die1);
            }
            else
            {
                Debug.LogError("erro neste Game Object" + gameObject.name);
            }
        }
        else
        {
            if (src != null && Die2 != null)
            {
                src.PlayOneShot(Die2);
            }
            else
            {
                Debug.LogError("erro neste Game Object" + gameObject.name);
            }
        }

        dieSound = !dieSound;
    }

    private void TakeDamageSound()
    {
        switch (Random.Range(1, 7))
        {
            case 1:
                {
                    if (src != null && Damage1 != null)
                    {
                        src.PlayOneShot(Damage1);
                    }
                    else
                    {
                        Debug.LogError("erro neste Game Object" + gameObject.name);
                    }
                    break;
                }
            case 2:
                {
                    if (src != null && Damage2 != null)
                    {
                        src.PlayOneShot(Damage2);
                    }
                    else
                    {
                        Debug.LogError("erro neste Game Object" + gameObject.name);
                    }
                    break;
                }
            case 3:
                {
                    if (src != null && Damage3 != null)
                    {
                        src.PlayOneShot(Damage3);
                    }
                    else
                    {
                        Debug.LogError("erro neste Game Object" + gameObject.name);
                    }
                    break;
                }
            case 4:
                {
                    if (src != null && Damage4 != null)
                    {
                        src.PlayOneShot(Damage4);
                    }
                    else
                    {
                        Debug.LogError("erro neste Game Object" + gameObject.name);
                    }
                    break;
                }
            case 5:
                {
                    if (src != null && Damage5 != null)
                    {
                        src.PlayOneShot(Damage5);
                    }
                    else
                    {
                        Debug.LogError("erro neste Game Object" + gameObject.name);
                    }
                    break;
                }
            case 6:
                {
                    if (src != null && Damage6 != null)
                    {
                        src.PlayOneShot(Damage6);
                    }
                    else
                    {
                        Debug.LogError("erro neste Game Object" + gameObject.name);
                    }
                    break;
                }
        }
    }
}
