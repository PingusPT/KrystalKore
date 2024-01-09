using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ColorAura : MonoBehaviour
{
    
    Light2D light2D;
    Color initialColor;
    public Color CorAzul = new Color(0, 222, 255, 127);
    public Color CorVermelho = new Color(255, 0, 0,127);
    public Color CorRoxo = new Color(255, 0, 255,127);
    public Color CorWhite = new Color(255, 255, 255);

    public float duration = 1f;
    float intenceDuration = 0.3f;
    bool isChangingColor = false;
    bool BlueInRange = false;
    bool PurpleInRange = false;
    bool pressing = false;

    float TimeBetweenKeysQtoE = 1.6f;
    float TimeBetweenKeysEtoQ = 1.6f;
   
    


    // Start is called before the first frame update
    void Start()
    {
        light2D = gameObject.GetComponent<Light2D>();
        light2D.color = Color.white;
        initialColor = light2D.color;
    }

    // Update is called once per frame
    void Update()
    {
       

        //---------------------------------------------------------------------------------------------------------

        if (Input.GetKey(KeyCode.E) && BlueInRange)
        {
            
            
            CristalControler.instance.myDelegateGrow();

        }
        else if (Input.GetKey(KeyCode.Q) && BlueInRange)
        {
           
            
            CristalControler.instance.Shrink();

        }
        else
        {

            CristalControler.instance.DelegateStop();
        }
        //&& CanGrowRoxo && hasPurpleArm ----------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.R) )
        {
            CristalRoxoController.instance.myDelegateAppear();

            StartCoroutine(SoftColorChange(CorRoxo));
            //StartCoroutine(ChangeIntensityOverTime(0.66f));
            initialColor = CorRoxo;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            StartCoroutine(SoftColorChange(CorVermelho));
            //StartCoroutine(ChangeIntensityOverTime(0.66f));
            initialColor = CorVermelho;
        }

        //----------------------------------------------------------------------------


        if (Input.GetKeyUp(KeyCode.Q))
        {

            pressing = false;
            TimeBetweenKeysQtoE = Time.time + 1.6f;

        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            pressing = false;

            TimeBetweenKeysEtoQ = Time.time + 1.6f;

        }

        Debug.Log(light2D.color == CorAzul);
        Debug.Log("isPressing = " + pressing);
        Debug.Log("The InitialColor is - " + initialColor);
        //------------------------------------------------------ Caso o Tempo seja maior q x voltar cor normal --------------------------------------------- Fazer mais um bool para saber se esta pressed
        
        if((Time.time - (TimeBetweenKeysEtoQ - 1.6f) < 1.5f || Time.time - (TimeBetweenKeysQtoE - 1.6f) < 1.5f) && Time.time > 1.6f)
        {
            
            //pressing = true;
            
            
            Debug.Log("TempoCurto");
            isChangingColor = true;
        }
        else
        {
            Debug.Log("TempoLongo");
            isChangingColor = false;
            if(light2D.color == CorAzul && !pressing && initialColor != CorWhite)
            {
                Debug.Log("CHANGE COLORRRRRRRRRR");
                ChangeNormalColor();
            }
            //ChangeNormalColor();
        }

       
        if ((Input.GetKeyDown(KeyCode.Q) && BlueInRange || Input.GetKeyDown(KeyCode.E)) && BlueInRange) //Se Q ou E esta clicado e 1 dos cristais azuis on range  
        {
            pressing = true;

            if((light2D.color != CorAzul))
            {

                Debug.Log("TrocarPAra Azul");
                StopAllCoroutines();
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


                elapsedTime += Time.deltaTime * 2;
                yield return null;
            }

            // Garante que a cor e intensidade finais sejam exatamente as alvo
            light2D.color = targetColor;
        }
    }

    IEnumerator ChangeIntensityOverTime(float targetIntensity)
    {
        //yield return new WaitForSeconds(0.5f);

        float elapsedTime = 0f;
        float initialIntensity = light2D.intensity;
        

        while (elapsedTime < intenceDuration)
        {
            float t = elapsedTime / duration;
            light2D.intensity = Mathf.Lerp(initialIntensity, targetIntensity, t);

            elapsedTime += Time.deltaTime * 2;
            yield return null;
        }

        // Garante que a intensidade final seja exatamente a alvo
        light2D.intensity = targetIntensity;
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

    public void CantGrowPurple()
    {
        isChangingColor = false;
        PurpleInRange = false;
    }
    
    private void ChangeNormalColor()
    {
        
        
            

            StopAllCoroutines();
            
            StartCoroutine(SoftColorChange(CorWhite));
            //StartCoroutine(ChangeIntensityOverTime(4f));
            initialColor = Color.white;
            
        
        
    }

    

}
