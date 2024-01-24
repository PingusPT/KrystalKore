
using UnityEngine;

public class CristalRoxoController : MonoBehaviour
{
   


    
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
            
        }


        //DontDestroyOnLoad(gameObject);
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
