using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ColorAura : MonoBehaviour
{
    public AnimationCurve intensityCurve;
    public AnimationCurve DiferentCurve;

    Light2D light2D;
    Color initialColor;
    public Color CorAzul = new Color(0, 222, 255, 127);
    public Color CorVermelho = new Color(255, 0, 0,127);
    public Color CorRoxo = new Color(255, 0, 255,127);
    public Color CorWhite = new Color(255, 255, 255);
    public float MaxChangeTime = 0.5f;

    public float duration = 1f;
    

    
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
        light2D.color = Color.white;
        initialColor = light2D.color;

        TimeBetweenKeysEtoQ = MaxChangeTime;
        TimeBetweenKeysQtoE = MaxChangeTime;


    }

    // Update is called once per frame
    void Update()
    {
       if((Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)) && BlueInRange && light2D.color != CorAzul && initialColor != CorAzul)
       {
            
            StartCoroutine(SoftColorChange(CorAzul));
            initialColor = CorAzul;
       }

        //---------------------------------------------------------------------------------------------------------

        if (Input.GetKey(KeyCode.E) && BlueInRange)
        {

            pressing = true;
            CristalControler.instance.myDelegateGrow();

        }
        else if (Input.GetKey(KeyCode.Q) && BlueInRange)
        {

            pressing = true;
            CristalControler.instance.Shrink();

        }
        else
        {

            CristalControler.instance.DelegateStop();
        }
        //&& CanGrowRoxo && hasPurpleArm ----------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.R) && PurpleInRange && hasPurplePower)
        {
            CristalRoxoController.instance.myDelegateAppear();
            StartCoroutine(ChangeIntensityOverTime(bottomIntenity, false));
            StartCoroutine(SoftColorChange(CorRoxo));
            //StartCoroutine(ChangeIntensityOverTime(0.66f));
            initialColor = CorRoxo;
        }

        if (Input.GetKeyDown(KeyCode.V) && RedInRange && hasRedArm)
        {
            StartCoroutine(SoftColorChange(CorVermelho));
            initialColor = CorVermelho;
            RedCristalController.instance.ActivateBombs();

            
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
            
            //pressing = true;
            
            
            
            isChangingColor = true;
        }
        else
        {
            
            isChangingColor = false;
            if(light2D.color == CorAzul && !pressing && initialColor != CorWhite)
            {
                
                initialColor = CorWhite;
                ChangeNormalColor();
            }
            //ChangeNormalColor();
        }

       
        if ((Input.GetKeyDown(KeyCode.Q) && BlueInRange || Input.GetKeyDown(KeyCode.E)) && BlueInRange) //Se Q ou E esta clicado e 1 dos cristais azuis on range  
        {
            pressing = true;

            if((light2D.color != CorAzul))
            {

                
                StopAllCoroutines();
                //StartCoroutine(ChangeIntensityOverTime(bottomIntenity, false)); -------------------------------------------- Por isto no Pressinggggggggggg
                StartCoroutine(SoftColorChange(CorAzul));
                
                initialColor = CorAzul;
                isChangingColor = true;
            }
            

        }
       
    }

    
    IEnumerator SoftColorChange(Color targetColor)
    {
        
        if(!isChangingColor)
        {
            Color initialColor = light2D.color;

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                light2D.color = Color.Lerp(initialColor, targetColor, t);


                elapsedTime += Time.deltaTime *2.5f;
                yield return null;
            }

            // Garante que a cor e intensidade finais sejam exatamente as alvo
            light2D.color = targetColor;
        }
    }

    IEnumerator ChangeIntensityOverTime(float targetIntensity, bool Inverted)
    {
        float initialIntensity = 3.07f;
        float elapsedTime = 0f;

        if (!Inverted)
        {
            initialIntensity = 3.07f;

            light2D.intensity = targetIntensity;

           
        }
        else
        {
            

            light2D.intensity = DiferentCurve.Evaluate(0f);
           // Debug.Log("Intencidade da Luz - " + light2D.intensity);
        }
        

        /*
        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            light2D.intensity = Mathf.Lerp(targetIntensity, initialIntensity, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Garante que a intensidade final seja exatamente a original
        light2D.intensity = initialIntensity;
        */
        
        if(!Inverted)
        {
            while (light2D.intensity != initialIntensity)
            {

                light2D.intensity = intensityCurve.Evaluate(elapsedTime);

                //light2D.intensity = Mathf.Lerp(targetIntensity, initialIntensity, curveValue);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Garante que a intensidade final seja exatamente a original
            light2D.intensity = initialIntensity;
        }
        else
        {
            while (elapsedTime < 0.8f)
            {
                
                light2D.intensity = DiferentCurve.Evaluate(elapsedTime);

                //light2D.intensity = Mathf.Lerp(targetIntensity, initialIntensity, curveValue);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Garante que a intensidade final seja exatamente a original
            light2D.intensity = initialIntensity;
        }
        
    }

    

    public void CantGrow()
    {
        isChangingColor = false;

        BlueInRange = false;
        
        ChangeNormalColor();
        
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


        

        //StopAllCoroutines();
        if(light2D.color != CorWhite)
        {
            StartCoroutine(ChangeIntensityOverTime(bottomIntenity, true));

        }
        StartCoroutine(SoftColorChange(CorWhite));

        StopCoroutine(SoftColorChange(CorWhite));
        //StartCoroutine(ChangeIntensityOverTime(4f));
        initialColor = CorWhite;
            
        
        
    }

    public void CatchRedArm()
    {
        Debug.Log("Apanhou o braço vermelho");
        hasRedArm = true;
    }

    public void CatchPuprlePower()
    {
        Debug.Log("Apanhou o poder roxo");
        hasPurplePower = true;
    }
    
    public void SetColorAuraProperties(bool HasRedArm, bool HasPurplePower)
    {
        hasPurplePower = HasPurplePower;
        hasRedArm = HasRedArm;
    }
}
