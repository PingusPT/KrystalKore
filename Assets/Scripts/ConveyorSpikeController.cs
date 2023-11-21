using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorSpikeController : MonoBehaviour
{
    GameObject[] Conveyor1;
    GameObject[] Conveyor2;
    float SpeedConveyor1 = 0.5f;
    float SpeedConveyor2 = 0.5f;

    public static ConveyorSpikeController instance;
    public delegate void MultiDelegate();
    public MultiDelegate myDelegateChangeRotationSpikes1;
    public MultiDelegate myDelegateChangeRotationSpikes2;
    public MultiDelegate myDelegateChangeRotationSpikes3;
    public MultiDelegate myDelegateChangeRotationSpikes4;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Sticky[] allObjsAry = Resources.FindObjectsOfTypeAll(typeof(Sticky)) as Sticky[];

        Conveyor1 = GameObject.FindGameObjectsWithTag("Conveyor1");
        Conveyor2 = GameObject.FindGameObjectsWithTag("Conveyor2");
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
            }
            else if(allObjsAry[i].gameObject.tag == "Spike4")
            {
                myDelegateChangeRotationSpikes4 += allObjsAry[i].GetComponent<Sticky>().ChangeDirection;
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
        myDelegateChangeRotationSpikes3();
    }
    public void ChangeDir4()
    {
        myDelegateChangeRotationSpikes4();
    }
}
