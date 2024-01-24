
using UnityEngine;

public class CristalControler : MonoBehaviour
{
    // Start is called before the first frame update

    
    

    
    public static CristalControler instance;
    public delegate void MultiDelegate();
    public MultiDelegate myDelegateGrow;
    public  MultiDelegate myDelegateShrink;
    public MultiDelegate myDelegateStop;

    void Start()
    {
        CristalAzul[] allObjsAry = Resources.FindObjectsOfTypeAll(typeof(CristalAzul)) as CristalAzul[];
        for (var i = 0; i < allObjsAry.Length ; ++i)
        {
            if(allObjsAry[i] != null)
            {
               
                myDelegateShrink += allObjsAry[i].GetComponent<CristalAzul>().Srink;
                myDelegateGrow += allObjsAry[i].GetComponent<CristalAzul>().Grow;
                myDelegateStop += allObjsAry[i].GetComponent<CristalAzul>().StopAnimation;
                
            }

            
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

        myDelegateStop();
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
