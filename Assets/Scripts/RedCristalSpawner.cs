using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCristalSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject redCristal;
    [SerializeField] GameObject Point;
    public static RedCristalSpawner instance;

    bool destroid = false;
    float time = 4f;

    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(destroid)
        {
            time -= Time.deltaTime;

            if(time < 0)
            {
                redCristal.SetActive(true);
                redCristal.transform.position = Point.transform.position;
                destroid = false;
                time = 4f;
            }
        }



    }


    public void GetRedCristal(GameObject gameObject)
    {
        redCristal = gameObject;
    }

    public void isDestroid()
    {
        destroid = true;
    }
}
