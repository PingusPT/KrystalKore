using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AberturasExplo : MonoBehaviour
{
    [SerializeField] GameObject otherObject;
    AberturasExplo otherScript;
    SpriteRenderer render;
    Color transparent;

    bool flag = true;

    bool destroid = false;

    bool ativarCor = false;

    bool otherObjectDestroid = false;

    float delay = 3f;


    // Start is called before the first frame update
    void Start()
    {
        render = gameObject.GetComponent<SpriteRenderer>();
        transparent = new Color(render.color.r, render.color.g, render.color.b, 0f);

        otherScript = otherObject.GetComponent<AberturasExplo>();

    }

    // Update is called once per frame
    void Update()
    {
        if(destroid)
        {
            delay -= Time.deltaTime;

            if(delay < 0)
            {
                delay = 3f;
                destroid = false;
                ativarCor = true;
            }
        }
         
        if(ativarCor)
        {
            delay -= Time.deltaTime;

            transparent = new Color(render.color.r, render.color.g, render.color.b, Mathf.Lerp(transparent.a, 1, 0.005f));
            render.color = transparent;
            if (delay < 0)
            {
                delay = 3;
                ativarCor = false;
            }

            

        }

        if(otherScript.isDestroid() && destroid && flag)
        {
            flag = false;
            
        }

    }

    public void Ativacao()
    {

        transparent = new Color(render.color.r, render.color.g, render.color.b, 0f);
        render.color = transparent;

        destroid = true;

    }

    public bool isDestroid()
    {
        return destroid;
    }

}
