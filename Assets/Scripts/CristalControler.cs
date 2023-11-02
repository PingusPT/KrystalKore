using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalControler : MonoBehaviour
{
    // Start is called before the first frame update

    List<CristalAzul> results = new List<CristalAzul>();
    

    [SerializeField] GameObject Cristal;
    public static CristalControler instance;
    public delegate void MultiDelegate();
    public MultiDelegate myDelegateGrow;
    public  MultiDelegate myDelegateShrink;
    public MultiDelegate myDelegateStop;

    void Start()
    {
        CristalAzul[] allObjsAry = Resources.FindObjectsOfTypeAll(typeof(CristalAzul)) as CristalAzul[];
        for (var i = 0; i < allObjsAry.Length; ++i)
        {
            if (string.IsNullOrEmpty(allObjsAry[i].transform.root.gameObject.scene.name))
                continue;
            myDelegateShrink += allObjsAry[i].GetComponent<CristalAzul>().Srink;
            myDelegateGrow += allObjsAry[i].GetComponent<CristalAzul>().Grow;
            myDelegateStop += allObjsAry[i].GetComponent<CristalAzul>().StopAnimation;
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

        myDelegateStop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shrink()
    {
        myDelegateShrink();
    }

    public void Grow()
    {
        myDelegateGrow();
    }
    public void DelegateStop()
    {
        myDelegateStop();
    }
}
