using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    Aura aura;
    [SerializeField] GameObject GameObjectLuz;
    [SerializeField] private LayerMask ignorMe;
    [SerializeField] private LayerMask ignorMeGrab;
    [SerializeField] GameObject GrabPoint;
    [SerializeField] GameObject JumpPoint;

    Light2D luz;
    GameObject GrabedObject;
    Animator anim;

    private RaycastHit2D rayGrab;
    private RaycastHit2D rayHit;
    
    Rigidbody2D rgd;

    Color CorAzul = new Color(0, 222, 255);
    Color CorVermelho = new Color(255, 15, 0);
    Color CorRoxo = new Color(255, 0, 255);
    Color CorAtual = new Color();

    Vector2 movement;

    float dir = 0;

    float horizontal;
    float Vertical;
    bool ground = false;
    [SerializeField] float speed = 10f;
    [SerializeField] float JumpForce = 600;
    bool CanGrowRoxo = false;
    bool CanGrowBlue = false;
    bool hasLegs = false;
    bool hasPurpleArm = false;
    bool PlayerOnWater = false;
    bool CanWalk = true;

    // Start is called before the first frame update
    void Start()
    {
        
        luz = GameObjectLuz.GetComponent<Light2D>();
        anim = gameObject.GetComponent<Animator>();
        aura = gameObject.GetComponentInChildren<Aura>();
        rgd = gameObject.GetComponent<Rigidbody2D>();
        CorAtual = CorAzul;

    }

    // Update is called once per frame
    void Update()
    {
        if (hasLegs)
        {
            CanWalk = true;
            speed = 6f;
        }

        rayHit = Physics2D.Raycast(JumpPoint.transform.position, -transform.up, 1f, ignorMe);
        Debug.DrawRay(JumpPoint.transform.position, -transform.up * 1f, Color.red);


        if (rayGrab.collider != null && rayGrab.collider.gameObject.layer == 8)
        {
            
            
            if (Input.GetMouseButton(0))
            {
                if(!GrabedObject)
                {
                    GrabedObject = rayGrab.collider.gameObject;
                    GrabedObject.transform.SetParent(GrabPoint.transform);
                }
                

                if (GrabedObject.GetComponent<Rigidbody2D>().gravityScale > 0)
                {
                    GrabedObject.GetComponent<Rigidbody2D>().gravityScale = 0;

                }

                GrabedObject.transform.position = GrabPoint.transform.position;
                
            }
            else
            {
                if(GrabedObject)
                {
                    if (GrabedObject.GetComponent<Rigidbody2D>().gravityScale == 0)
                    {
                        GrabedObject.GetComponent<Rigidbody2D>().gravityScale = 1;

                    }

                    GrabedObject.transform.SetParent(null);
                    GrabedObject = null;
                }
               

            }
        }
        
        
        if(rayHit.distance < 0.56f && rayHit.distance != 0)
        {
            
            ground = true;
        }
        else
        {
            
            ground = false;

        }
        
        if (Input.GetButtonDown("Jump") && ground && hasLegs)
        {
            if(PlayerOnWater)
            {
                JumpForce = 900;
                
            }
            else
            {
                JumpForce = 600;
            }
            rgd.AddForce(transform.up * JumpForce);
           
        }
        if(Input.GetKey(KeyCode.E) && CanGrowBlue)
        {
            luz.color = Color.Lerp(CorAtual, CorAzul, 0.1f);
            luz.intensity = 0.002f;
            CristalControler.instance.myDelegateGrow();
            
        }
        else if (Input.GetKey(KeyCode.Q) && CanGrowBlue)
        {
            luz.color = Color.Lerp(CorAtual, CorAzul, 0.1f);
            luz.intensity = 0.002f;
            CristalControler.instance.Shrink();
            
        }
        else
        {
            
            CristalControler.instance.DelegateStop();
        }

        if(Input.GetKeyDown(KeyCode.R) && CanGrowRoxo && hasPurpleArm)
        {
            luz.color = Color.Lerp(CorAtual, CorRoxo, 0.1f);
            luz.intensity = 0.002f;
            CristalRoxoController.instance.myDelegateAppear();
        }

        

    }
    private void FixedUpdate()
    { 

        //--------------------------------------------------------------------------------------- MovementZone ---------------------------------------------------------------

        horizontal = Input.GetAxis("Horizontal");

        if(PlayerOnWater)
        {
            Vertical = Input.GetAxis("Vertical");
            Vertical *= 7;
        }
        else
        {
            Vertical = 0;
        }


        if (CanWalk)
        {
            movement = new Vector2(speed * horizontal, Vertical);

            movement *= Time.deltaTime;
           
           
        }
        else
        {
            movement = Vector2.zero;
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //------------------------------------------------------------------------------  Grab Hability -----------------------------------------------------------------------
        if (movement.x > 0f)
        {
            rayGrab = Physics2D.Raycast(GrabPoint.transform.position, -transform.right, 1.2f, LayerMask.GetMask("Objects"));
            Debug.DrawRay(GrabPoint.transform.position, -transform.right * 1.2f, Color.green);

            if (GrabPoint.transform.localPosition.x < 0)
            {
                GrabPoint.transform.localPosition = new Vector2(GrabPoint.transform.localPosition.x * -1, GrabPoint.transform.localPosition.y);
            }
            dir = 1;
        }
        else if(movement.x < 0f)
        {
            rayGrab = Physics2D.Raycast(GrabPoint.transform.position, transform.right, 1.2f, LayerMask.GetMask("Objects"));
            Debug.DrawRay(GrabPoint.transform.position, transform.right * 1.2f, Color.green);

            if (GrabPoint.transform.localPosition.x > 0)
            {
                GrabPoint.transform.localPosition = new Vector2(GrabPoint.transform.localPosition.x * -1, GrabPoint.transform.localPosition.y);
            }
            dir = -1;
        }
        else
        {
            if(dir == 1)
            {
                rayGrab = Physics2D.Raycast(GrabPoint.transform.position, -transform.right, 1.2f, LayerMask.GetMask("Objects"));
                Debug.DrawRay(GrabPoint.transform.position, -transform.right * 1.2f, Color.green);
            }
            else
            {
                rayGrab = Physics2D.Raycast(GrabPoint.transform.position, transform.right, 1.2f, LayerMask.GetMask("Objects"));
                Debug.DrawRay(GrabPoint.transform.position, transform.right * 1.2f, Color.green);

            }
        }
        
       //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        

        //----------------------------------------------------------------------  Animation Zone ----------------------------------

        if (movement == Vector2.zero)
        {
            //anim.SetFloat("velocity", 0);
        }
        else if (dir > 0)
        {
            //anim.SetFloat("velocity", 1);
            anim.SetFloat("Blend", 1);
        }
        else
        {
            //anim.SetFloat("velocity", 1);
            anim.SetFloat("Blend", -1);
        }

        //-------------------------------------------------------------------------------------------------------------------------

        transform.Translate(movement);

    }

    

    public void AutorizarAzul()
    {
        CanGrowBlue = true;
        
    }
    public void AutorizarRoxo()
    {
        CanGrowRoxo = true;
        
    }

    public void DesauturizarAzul()
    {
        CanGrowBlue = false;
        
    }
    public void DesautorizarRoxo()
    {
        CanGrowRoxo = false;
        
    }

    public void CatchLegs()
    {
        CanWalk = true;
        hasLegs = true;
        anim.SetBool("HasLegs", hasLegs);
    }

    public void CatchPurpleArm()
    {
        hasPurpleArm = true;
    }

    private void Walk()
    {
        CanWalk = true;
    }

    private void canotWalk()
    {
        CanWalk = false;
    }
    public void ChangeColorToRed()
    {
        luz.color = Color.Lerp(CorAtual, CorVermelho, 10);
        luz.intensity = 0.002f;
    }


    public void IsOnWater()
    {
        PlayerOnWater = true;
        ground = true;
    }

    public void IsNotOnWater()
    {
        PlayerOnWater = false;
        ground = false;
    }
}
