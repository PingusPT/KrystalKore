using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BluePulseLight : MonoBehaviour
{

    Light2D light2d;

    public AnimationCurve animationCurve;

    public float StartLightIntensity = 4.5f;
    public float EndLightIntensity = 7.5f;
    public float SwitchIntensityTime = 1;

    [Range(4.5f, 20f)]
    public float CurveWeigth = 6f;

    bool isGrow = false;
    bool isShrink = false;

    float time = 0;

    // Start is called before the first frame update
    void Start()
    {

        light2d = gameObject.GetComponent<Light2D>();

        light2d.intensity = StartLightIntensity;

        animationCurve.ClearKeys();

        animationCurve.AddKey(0f, StartLightIntensity);
        animationCurve.AddKey(SwitchIntensityTime, EndLightIntensity);

        animationCurve.AddKey(SwitchIntensityTime / 2f, CurveWeigth);

        MoreIntensity();
    }

    private void Update()
    {
        if(isGrow)
        {

            time += Time.deltaTime;
            light2d.intensity = animationCurve.Evaluate(time);

            

            if (light2d.intensity == EndLightIntensity)
            {
                isGrow = false;
                LessIntensity();
            }
            
        }
        if(isShrink)
        {

            time -= Time.deltaTime;
            light2d.intensity = animationCurve.Evaluate(time);

            if (light2d.intensity == StartLightIntensity)
            {
                isShrink = false;
                MoreIntensity();
            }
        }
    }

    private void MoreIntensity()
    {
        
        time = 0f;
        isGrow = true;
        
    }

    private void LessIntensity()
    {
        
        time = SwitchIntensityTime;
        isShrink = true;
    }
    /*
    private IEnumerator LessIntensity()
    {
        float time = SwitchIntensityTime;

        isShrink = true;

        while (light2d.intensity > StartLightIntensity)
        {
            time -= Time.deltaTime;
            light2d.intensity = animationCurve.Evaluate(time);
            Debug.Log("AAAA");
        }
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(MoreIntensity());
    }
    */
}
