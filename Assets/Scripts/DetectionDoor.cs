using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class DetectionDoor : MonoBehaviour
{

    [SerializeField] GameObject CamPoint;
    [SerializeField] GameObject Porta;
    [SerializeField] GameObject Camera;

    Animator anim;
    CinemachineVirtualCamera virtCam;
    CinemachineTransposer transposer;

    GameObject Player;
    
    

    bool flag = true;
    bool flag1 = true;

    bool SoftChange = false;
    bool VoltarAoNormal = false;

    
    float camLens = 7.5f;

    float time = 4f;
    bool CountDown = false;

    // Start is called before the first frame update
    void Start()
    {
        
        anim = Porta.GetComponent<Animator>();
        virtCam = Camera.GetComponent<CinemachineVirtualCamera>();
        transposer = virtCam.GetCinemachineComponent<CinemachineTransposer>();

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(SoftChange)
        {
            
            camLens = Mathf.Lerp(camLens, 15f, 0.02f);

            virtCam.m_Lens.OrthographicSize = camLens;

            if(virtCam.m_Lens.OrthographicSize > 14.89f)
            {
                SoftChange = false;
            }
        }

        if(VoltarAoNormal)
        {
            camLens = Mathf.Lerp(camLens, 7.25f, 0.02f);

            virtCam.m_Lens.OrthographicSize = camLens;

            if (virtCam.m_Lens.OrthographicSize < 7.24)
            {
                VoltarAoNormal = false;
            }
        }
        */
        if(CountDown)
        {
            
            time -= Time.deltaTime;

            
            if(time < 0)
            {
                Player.GetComponent<PlayerMovement>().enabled = true;
                CountDown = false;
                time = 4f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && flag)
        {
            Player = collision.gameObject;
            
            flag = false;
            /*
            SoftChange = true;

            transposer.m_XDamping = 5f;
            transposer.m_YDamping = 5f;
            transposer.m_ZDamping = 5f;

            

            virtCam.Follow = CamPoint.transform;
            */
            anim.SetTrigger("fechar");
            Player.GetComponent<PlayerMovement>().enabled = false;

            CountDown = true;
        }
        if(collision.gameObject.tag == "Chave" && flag1)
        {
            //virtCam.Follow = Player.transform;
            
            anim.SetTrigger("abrir");

            VoltarAoNormal = true;
            /*
            transposer.m_XDamping = 1f;
            transposer.m_YDamping = 1f;
            transposer.m_ZDamping = 1f;

            */
            Destroy(collision.gameObject);
        }
    }

}
