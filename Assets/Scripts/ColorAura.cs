using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ColorAura : MonoBehaviour
{
    public AnimationCurve intensityCurve;
    public AnimationCurve DiferentCurve;

    GameObject player;

    Animator anim;

    Light2D light2D;
    Color CurremtColor;
    public Color CorAzul = new Color(0, 222, 255, 127);
    public Color CorVermelho = new Color(255, 0, 0,127);
    public Color CorRoxo = new Color(255, 0, 255,127);
    public Color CorWhite = new Color(255, 255, 255);
    public float MaxChangeTime = 0.5f;

    public float duration = 1f;

    bool CR_running = false;
    
    bool isChangingColor = false;

    bool BlueInRange = false;
    bool PurpleInRange = false;
    bool RedInRange = false;

    public bool hasRedArm = false;
    public bool hasPurplePower = false;

    bool pressing = false;

    float TimeBetweenKeysQtoE;
    float TimeBetweenKeysEtoQ;

    public float bottomIntenity = 0.01f;


    // Start is called before the first frame update
    void Start()
    {
        light2D = gameObject.GetComponent<Light2D>();
        light2D.color = CorWhite;
        CurremtColor = light2D.color;

        TimeBetweenKeysEtoQ = MaxChangeTime;
        TimeBetweenKeysQtoE = MaxChangeTime;

        
    }

    // Update is called once per frame
    void Update()
    {
        if(anim == null)
        {
            anim = GameManagerScript.instance.moveScript.GetAnimator();
        }

        // Se o Q ou o E estiverem presionados, Tiver algum Cristal em Range --------------------------------------

       if((Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)) && BlueInRange && CurremtColor != CorAzul) // Apenas 1 frame
       {
            
            CurremtColor = CorAzul;
            Debug.Log("CorAzul");
            StartCoroutine(SoftColorChange(CorAzul));
       }

        
        //------------------------------------Part Funcional AZUL que os cristais efetivamente crescem --------------------------------------------------

        if (Input.GetKey(KeyCode.E) && BlueInRange)
        {
            anim.SetBool("usingBlue", true);
            pressing = true;
            CristalControler.instance.myDelegateGrow();

        }
        else if (Input.GetKey(KeyCode.Q) && BlueInRange)
        {
            anim.SetBool("usingBlue", true);
            pressing = true;
            CristalControler.instance.Shrink();

        }
        else
        {

            CristalControler.instance.DelegateStop();
        }
        //---------------------------------------------------- ----------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.R) && PurpleInRange && hasPurplePower) //Apenas 1 frame, troca a cor para roxo
        {
            CristalRoxoController.instance.myDelegateAppear();
            
            CurremtColor = CorRoxo;
            Debug.Log("CorRoxo");
            StartCoroutine(SoftColorChange(CorRoxo));
            

            
        }

        if (Input.GetKeyDown(KeyCode.V) && RedInRange && hasRedArm)
        {
            anim.SetTrigger("RedPower");
            Debug.Log("CorVermelho");
            CurremtColor = CorVermelho;
            StartCoroutine(SoftColorChange(CorVermelho));
            RedCristalController.instance.ActivateBombs();

            ChangeNormalColor();
            //StartCoroutine(ChangeIntensityOverTime(0.66f));
            
        }

        //----------------------------------------------------------------------------

        if (Input.GetKeyUp(KeyCode.Q))
        {

            pressing = false;
            TimeBetweenKeysQtoE = Time.time + MaxChangeTime;

        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            pressing = false;

            TimeBetweenKeysEtoQ = Time.time + MaxChangeTime;

        }

        
        //------------------------------------------------------ Caso o Tempo seja maior q x voltar cor normal --------------------------------------------- Fazer mais um bool para saber se esta pressed
        
        if((Time.time - (TimeBetweenKeysEtoQ - MaxChangeTime) < MaxChangeTime || Time.time - (TimeBetweenKeysQtoE - MaxChangeTime) < MaxChangeTime) && Time.time > MaxChangeTime)
        {
            anim.SetBool("usingBlue", true);
            isChangingColor = true;
        }
        else
        {
            //light2D.color == CorAzul && !pressing && CurremtColor != CorWhite
            isChangingColor = false;
            if(CurremtColor == CorAzul && !pressing)
            {
                anim.SetBool("usingBlue", false);
                
                ChangeNormalColor();
            }
            
        }

        if(!BlueInRange)
        {
            anim.SetBool("usingBlue", false);
        }

        /*
         if ((Input.GetKeyDown(KeyCode.Q) && BlueInRange || Input.GetKeyDown(KeyCode.E)) && BlueInRange) //Se Q ou E esta clicado e 1 dos cristais azuis on range  
         {
             pressing = true;

             if((light2D.color != CorAzul))
             {


                 CurremtColor = CorAzul;
                 StartCoroutine(SoftColorChange(CorAzul));

                 isChangingColor = true;
             }


         }
        */
    }

    
    IEnumerator SoftColorChange(Color targetColor)
    {
       
            if (!CR_running && targetColor != Color.clear)
            {
                CR_running = true;

                if (light2D.color == CorWhite)
                {
                    light2D.color = Color.clear;
                }
                if (targetColor == CorWhite)
                {

                    light2D.color = new Color(targetColor.r, targetColor.g, targetColor.b, 4f);
                    //light2D.color = CorRoxo;
                }

                if (!isChangingColor)
                {
                    Color initialColor2 = light2D.color;

                    float elapsedTime = 0f;

                    while (elapsedTime < duration)
                    {
                        float t = elapsedTime / duration;
                        light2D.color = Color.Lerp(initialColor2, targetColor, t);


                        elapsedTime += Time.deltaTime;
                        yield return null;
                    }

                    CR_running = false;
                    CurremtColor = targetColor;
                    light2D.color = targetColor;
                    
                    
                    if(Color.clear == light2D.color)
                    {

                        CurremtColor = CorAzul;
                        ChangeNormalColor();
                    }

                }

                CR_running = false;
                CurremtColor = targetColor;
                light2D.color = targetColor;
            }
            else
            {
                while (CR_running)
                {
                    yield return new WaitForSeconds(0.1f);
                }

                StartCoroutine(SoftColorChange(targetColor));

            }
        
    }
    
    public void CantGrow()
    {
        isChangingColor = false;

        BlueInRange = false;
        
        ChangeNormalColor();
        anim.SetBool("usingBlue", false);
    }

    public void CanGrow()
    {
        
        BlueInRange = true;
    }

    public void CanGrowPurple()
    {

        PurpleInRange = true;
    }

    public void CanExplodeRed()
    {
        RedInRange = true;
    }

    public void CantExplodeRed()
    {
        isChangingColor = false;

        RedInRange = false;

        ChangeNormalColor();
    }

    public void CantGrowPurple()
    {
        isChangingColor = false;

        PurpleInRange = false;

        ChangeNormalColor();
    }
    
    private void ChangeNormalColor()
    {
        if(CurremtColor != CorWhite)
        {
            CurremtColor = CorWhite;
            Debug.Log("CorBranco");
            
            StartCoroutine(SoftColorChange(CorWhite));
        }
        
       
    }

    public void CatchRedArm()
    {
        anim.SetBool("HasRedArm", true);
        hasRedArm = true;
    }

    public void CatchPuprlePower()
    {
       
        hasPurplePower = true;
    }
    
    public void SetColorAuraProperties(bool HasRedArm, bool HasPurplePower)
    {
        hasPurplePower = HasPurplePower;
        hasRedArm = HasRedArm;
    }

    
}
