using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamChange : MonoBehaviour
{
    [SerializeField] GameObject CamPoint;
    
    [SerializeField] GameObject Camera;

    
    CinemachineVirtualCamera virtCam;
    CinemachineTransposer transposer;

    

    GameObject Player;

    public AnimationCurve animationCurve;


    bool SoftChange = false;
    bool VoltarAoNormal = false;
    bool isCamChanged = false;
    
    public float DefaultCamLens = 7.5f;
    float camLens = 7.5f;

    [Range(0f, 2f)] public float percentageGrow = 0.8f;

    public bool DestroiAfertUse = false;
    public bool ZoomOut = true;
    public float SetTimeCurve = 1.5f;
    float TimeCurve = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        
        virtCam = Camera.GetComponent<CinemachineVirtualCamera>();
        transposer = virtCam.GetCinemachineComponent<CinemachineTransposer>();
        
        
    }

    // Update is called once per frame
    void Update()
    {

        
        if (SoftChange)
        {

            
            camLens = animationCurve.Evaluate(TimeCurve);
            TimeCurve += Time.deltaTime;


            //virtCam.m_Lens.OrthographicSize = DefaultCamLens * (percentageGrow + camLens);

            virtCam.m_Lens.OrthographicSize = (ZoomOut) ? DefaultCamLens * (percentageGrow + camLens) : DefaultCamLens * ((camLens * 0.6f) + 0.4f);


            if (camLens >= 1)
            {
                
                //virtCam.m_Lens.OrthographicSize = DefaultCamLens * (percentageGrow + camLens);
                TimeCurve = SetTimeCurve;
                SoftChange = false;

                if(DestroiAfertUse)
                {
                    gameObject.SetActive(false);
                }
            }
            
        }

        if (VoltarAoNormal) // Go back to default 
        {
            
            camLens = animationCurve.Evaluate(TimeCurve);
            TimeCurve -= Time.deltaTime;

            virtCam.m_Lens.OrthographicSize = (!ZoomOut) ? DefaultCamLens * ((camLens * 0.6f) + 0.4f) : DefaultCamLens * (1 + camLens);

            if (camLens == 0)
            {
                TimeCurve = 0;
                VoltarAoNormal = false;
                
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isCamChanged && !SoftChange)
        {
            Player = collision.gameObject;
            SetCamDamping();
            if (ZoomOut)
            {
                virtCam.Follow = CamPoint.transform;
                TimeCurve = 0;
                isCamChanged = true;
                VoltarAoNormal = false;
                SoftChange = true;
            }
            else
            {
                isCamChanged = true;
                TimeCurve = SetTimeCurve;
                virtCam.Follow = Player.transform;
                SoftChange = false;
                VoltarAoNormal = true;
            }
            

        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isCamChanged)
        {
            ResetCamDamping();

            if(ZoomOut)
            {
                isCamChanged = false;
                TimeCurve = SetTimeCurve;
                virtCam.Follow = Player.transform;
                SoftChange = false;
                VoltarAoNormal = true;
            }
            else
            {
                virtCam.Follow = CamPoint.transform;
                TimeCurve = 0.5f;
                
                isCamChanged = false;
                VoltarAoNormal = false;
                SoftChange = true;
            }
   
        }
    }

    private void SetCamDamping()
    {
        transposer.m_XDamping = 5f;
        transposer.m_YDamping = 5f;
        transposer.m_ZDamping = 5f;

        transposer.m_FollowOffset.y = 0f;
    }


    private void ResetCamDamping()
    {
        transposer.m_XDamping = 1f;
        transposer.m_YDamping = 1f;
        transposer.m_ZDamping = 1f;

        transposer.m_FollowOffset.y = 4.7f; //default game
    }
}
