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

    [Range(0, 2f)] public float percentageGrow = 0.8f;

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


            virtCam.m_Lens.OrthographicSize = DefaultCamLens * (percentageGrow + camLens);
            
            if (camLens >= 1)
            {
                
                TimeCurve = SetTimeCurve;
                SoftChange = false;
            }
            
        }

        if (VoltarAoNormal)
        {
            
            camLens = animationCurve.Evaluate(TimeCurve);
            TimeCurve -= Time.deltaTime;

            // 13.5
            virtCam.m_Lens.OrthographicSize = DefaultCamLens * ( 1f + camLens);

            if (camLens == 0)
            {
                TimeCurve = 0;
                VoltarAoNormal = false;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isCamChanged)
        {
            Player = collision.gameObject;

            SetCamDamping();
            virtCam.Follow = CamPoint.transform;
          
            isCamChanged = true;

            SoftChange = true;
           
            virtCam.Follow = CamPoint.transform;
          

            

        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isCamChanged)
        {
            ResetCamDamping();
            isCamChanged = false;

            virtCam.Follow = Player.transform;

            VoltarAoNormal = true;

            

        }
    }

    private void SetCamDamping()
    {
        transposer.m_XDamping = 5f;
        transposer.m_YDamping = 5f;
        transposer.m_ZDamping = 5f;
    }


    private void ResetCamDamping()
    {
        transposer.m_XDamping = 1f;
        transposer.m_YDamping = 1f;
        transposer.m_ZDamping = 1f;
    }
}
