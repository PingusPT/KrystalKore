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
    
    Light2D luz;
    GameObject GrabedObject;

    private RaycastHit2D rayGrab;
    private RaycastHit2D rayHit;
    
    Rigidbody2D rgd;

    Color CorAzul = new Color(0, 222, 255);
    Color CorVermelho = new Color(255, 15, 0);
    Color CorRoxo = new Color(255, 0, 255);
    Color CorAtual = new Color();

    float dir = 0;

    float horizontal;
    bool ground = false;
    [SerializeField] float speed = 10f;
    [SerializeField] float JumpForce = 600;
    bool CanGrowRoxo = false;
    bool CanGrowBlue = false;
    bool hasLegs = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
        luz = GameObjectLuz.GetComponent<Light2D>();
        
        aura = gameObject.GetComponentInChildren<Aura>();
        rgd = gameObject.GetComponent<Rigidbody2D>();
        CorAtual = CorAzul;

    }

    // Update is called once per frame
    void Update()
    {
        
        rayHit = Physics2D.Raycast(transform.position, -transform.up, 1f, ignorMe);
        Debug.DrawRay(transform.position, -transform.up * 1f, Color.red);
        if(rayGrab.collider != null && rayGrab.collider.gameObject.layer == 8)
        {
            //Input.GetMouseButton(0)
            
            if (Input.GetMouseButton(0))
            {
                
                GrabedObject = rayGrab.collider.gameObject;
                GrabedObject.transform.position = GrabPoint.transform.position;
                GrabedObject.transform.SetParent(GrabPoint.transform);
            }
            else
            {
                if(GrabedObject)
                {
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

        if(Input.GetKeyDown(KeyCode.R) && CanGrowRoxo)
        {
            luz.color = Color.Lerp(CorAtual, CorRoxo, 0.1f);
            luz.intensity = 0.002f;
            CristalRoxoController.instance.myDelegateAppear();
        }

        

    }
    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(speed * horizontal, 0);

        movement *= Time.deltaTime;

        if(movement.x > 0f)
        {
            rayGrab = Physics2D.Raycast(gameObject.transform.position, transform.right, 1.2f, LayerMask.GetMask("Objects"));
           
            GrabPoint.transform.localPosition = new Vector2(1.27f, 0);
            dir = 1;
        }
        else if(movement.x < 0f)
        {
            rayGrab = Physics2D.Raycast(gameObject.transform.position, -transform.right, 1.2f, LayerMask.GetMask("Objects"));
            
            GrabPoint.transform.localPosition = new Vector2(-1.27f, 0);
            dir = -1;
        }
        else
        {
            if(dir == 1)
            {
                rayGrab = Physics2D.Raycast(gameObject.transform.position, transform.right, 1.2f, LayerMask.GetMask("Objects"));
                
            }
            else
            {
                rayGrab = Physics2D.Raycast(gameObject.transform.position, -transform.right, 1.2f, LayerMask.GetMask("Objects"));
                
            }
        }

        

        
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
        hasLegs = true;
    }

    

    public void ChangeColorToRed()
    {
        luz.color = Color.Lerp(CorAtual, CorVermelho, 10);
        luz.intensity = 0.002f;
    }
}
