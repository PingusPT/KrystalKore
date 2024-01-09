using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalRoxoController : MonoBehaviour
{
    List<CristalRoxo> results = new List<CristalRoxo>();


    
    public static CristalRoxoController instance;
    public delegate void MultiDelegate();
    public MultiDelegate myDelegateAppear;
    
    

    void Start()
    {
        CristalRoxo[] allObjsAry = Resources.FindObjectsOfTypeAll(typeof(CristalRoxo)) as CristalRoxo[];
        for (var i = 0; i < allObjsAry.Length; ++i)
        {
            if (string.IsNullOrEmpty(allObjsAry[i].transform.root.gameObject.scene.name))
                continue;
            myDelegateAppear += allObjsAry[i].GetComponent<CristalRoxo>().Appear;
            
            
            results.Add(allObjsAry[i]);
        }


        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

}
