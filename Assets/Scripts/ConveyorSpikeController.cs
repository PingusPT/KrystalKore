using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorSpikeController : MonoBehaviour
{
    GameObject[] Conveyor1;
    GameObject[] Conveyor2;
    GameObject Conveyor3;
    float SpeedConveyor1 = 0.5f;
    float SpeedConveyor2 = 0.5f;

    public static ConveyorSpikeController instance;
    public delegate void MultiDelegate();
    public MultiDelegate myDelegateChangeRotationSpikes1;
    public MultiDelegate myDelegateChangeRotationSpikes2;
    public MultiDelegate myDelegateChangeRotationSpikes3;
    public MultiDelegate myDelegateAcivate;

    bool terceiro = false;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Sticky[] allObjsAry = Resources.FindObjectsOfTypeAll(typeof(Sticky)) as Sticky[];

        Conveyor1 = GameObject.FindGameObjectsWithTag("Conveyor1");
        Conveyor2 = GameObject.FindGameObjectsWithTag("Conveyor2");
        Conveyor3 = GameObject.FindGameObjectWithTag("Conveyor3");
        for (var i = 0; i < allObjsAry.Length; ++i)
        {
            if (string.IsNullOrEmpty(allObjsAry[i].transform.root.gameObject.scene.name))
                continue;
            if(allObjsAry[i].gameObject.tag == "Spike1")
            {
                myDelegateChangeRotationSpikes1 += allObjsAry[i].GetComponent<Sticky>().ChangeDirection;
            }
            else if(allObjsAry[i].gameObject.tag == "Spike2")
            {
                myDelegateChangeRotationSpikes2 += allObjsAry[i].GetComponent<Sticky>().ChangeDirection;
            }
            else if(allObjsAry[i].gameObject.tag == "Spike3")
            {
                myDelegateChangeRotationSpikes3 += allObjsAry[i].GetComponent<Sticky>().ChangeDirection;
                myDelegateAcivate += allObjsAry[i].GetComponent<Sticky>().Walk;
                allObjsAry[i].GetComponent<Sticky>().Stop();
            }
            
            

        }
    }

    public void ChangeDir1()
    {
        SpeedConveyor1 *= -1;

        for(int i = 0; i < Conveyor1.Length; ++i)
        {
            Conveyor1[i].GetComponent<SurfaceEffector2D>().speed = SpeedConveyor1;
        }

        myDelegateChangeRotationSpikes1();
    }
    public void ChangeDir2()
    {
        SpeedConveyor2 *= -1;

        for (int i = 0; i < Conveyor2.Length; ++i)
        {
            Conveyor2[i].GetComponent<SurfaceEffector2D>().speed = SpeedConveyor2;
        }

        myDelegateChangeRotationSpikes2();
    }
    public void ChangeDir3()
    {
        if(terceiro)
        {
            Conveyor3.GetComponent<SurfaceEffector2D>().speed = SpeedConveyor1;
            myDelegateChangeRotationSpikes3();
        }
       
    }
    
    public void AticarTereciro()
    {
        terceiro = true;

        myDelegateAcivate();
    }
}
