using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCristalController : MonoBehaviour
{

    public static RedCristalController instance;

    public delegate void MyMultiDelegate();
    public MyMultiDelegate delegateExplode;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        RedCristal[] allObjsAry = Resources.FindObjectsOfTypeAll(typeof(RedCristal)) as RedCristal[];
        for (var i = 0; i < allObjsAry.Length; ++i)
        {
            if (allObjsAry[i] != null)
            {
                delegateExplode += allObjsAry[i].GetComponent<RedCristal>().StartExplo;
                
            }


        }

        Debug.Log("Ha " + allObjsAry.Length + " Cristais Vermelho");
    }

    public void ActivateBombs()
    {
        delegateExplode();
    }
}
