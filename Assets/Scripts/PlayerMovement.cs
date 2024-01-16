using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    
    
    [SerializeField] private LayerMask ignorMe;
    [SerializeField] private LayerMask ignorMeGrab;
    [SerializeField] private LayerMask GrabNothing;
    [SerializeField] private LayerMask GrabAll;

    [SerializeField] GameObject GrabPoint;
    [SerializeField] GameObject JumpPoint;

    
    GameObject GrabedObject;

    Rigidbody2D rgbGrabed;
    CircleCollider2D colliderGrabed;

    Animator anim;

    [SerializeField] float speed = 10f;
    [SerializeField] float JumpForce = 600;

    private RaycastHit2D rayGrab;
    private RaycastHit2D rayHit;
    
    Rigidbody2D rgd;

    
    Vector2 movement;

    float dir = 0;

    float horizontal;
    float Vertical;
    bool ground = false;
    
    public bool hasLegs = false;

    bool stopGrabing = false;
    bool PlayerOnWater = false;
    bool CanWalk = true;

    // Start is called before the first frame update
    void Start()
    {
        
        
        anim = gameObject.GetComponent<Animator>();
        
        rgd = gameObject.GetComponent<Rigidbody2D>();
        

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
        Debug.Log(rayHit.collider);

        if (rayGrab.collider != null && rayGrab.collider.gameObject.layer == 8)
        {
            
            
            if (Input.GetMouseButton(0) && !stopGrabing)
            {
                if(!GrabedObject)
                {
                    
                    GrabedObject = rayGrab.collider.gameObject;
                    rgbGrabed = GrabedObject.GetComponent<Rigidbody2D>();
                    colliderGrabed = GrabedObject.GetComponent<CircleCollider2D>();
                    GrabedObject.transform.SetParent(GrabPoint.transform);
                }
                

                if (rgbGrabed.bodyType == RigidbodyType2D.Dynamic)
                {
                    //GrabedObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                    rgbGrabed.bodyType = RigidbodyType2D.Kinematic;
                    colliderGrabed.forceReceiveLayers = GrabNothing;
                    colliderGrabed.forceSendLayers = GrabNothing;

                }

                GrabedObject.transform.position = GrabPoint.transform.position;
                
            }
            else
            {
                if(GrabedObject)
                {
                    DroppGrabed();
                }

            }
        }

        //&& rayHit.distance != 0

        if (rayHit.distance < 0.56f && rayHit.collider != null)
        {
            
            ground = true;
        }
        else
        {
            
            ground = false;

        }
        
        if (Input.GetButtonDown("Jump") && ground && hasLegs)
        {
            
            rgd.AddForce(transform.up * JumpForce);
           
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


    public void CatchLegs()
    {
        CanWalk = true;
        hasLegs = true;
        anim.SetBool("HasLegs", hasLegs);
    }

    public void DroppGrabed()
    {
        if(GrabedObject)
        {
            stopGrabing = true;
            rgbGrabed.bodyType = RigidbodyType2D.Dynamic;
            colliderGrabed.forceReceiveLayers = GrabAll;
            colliderGrabed.forceSendLayers = GrabAll;
            GrabedObject.transform.SetParent(null);
            GrabedObject = null;
            Debug.Log("drop");
            StartCoroutine(AllowGrabimg());
        }
        

    }

    private void Walk()
    {
        CanWalk = true;
    }

    private void canotWalk()
    {
        CanWalk = false;
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

    public void SetPlayerMovementPropreties(bool HasLegs, float PositionX, float PositionY)
    {
        hasLegs = HasLegs;
        if(hasLegs)
        {
            CatchLegs();
        }
       
        gameObject.transform.position = new Vector2(PositionX, PositionY);
    }


    private IEnumerator AllowGrabimg()
    {
        yield return new WaitForSeconds(0.3f);

        
        stopGrabing = false;
        
    }
}
