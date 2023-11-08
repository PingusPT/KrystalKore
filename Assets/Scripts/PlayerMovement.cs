using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Aura aura;

    Rigidbody2D rgd;

    float horizontal;
    bool ground;
    [SerializeField] float speed = 10f;
    [SerializeField] float JumpForce = 600;
    bool CanGrowRoxo = false;
    bool CanGrowBlue = false;
    bool hasLegs = false;

    // Start is called before the first frame update
    void Start()
    {
        aura = gameObject.GetComponentInChildren<Aura>();
        rgd = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(CanGrowRoxo);

        if (Input.GetButtonDown("Jump") && ground && hasLegs)
        {
            rgd.AddForce(transform.up * JumpForce);
            ground = false;
        }
        if(Input.GetKey(KeyCode.E) && CanGrowBlue)
        {
            
            CristalControler.instance.myDelegateGrow();
            
        }
        else if (Input.GetKey(KeyCode.Q) && CanGrowBlue)
        {
            CristalControler.instance.Shrink();
            
        }
        else
        {
            CristalControler.instance.DelegateStop();
        }

        if(Input.GetKeyDown(KeyCode.R) && CanGrowRoxo)
        {
            CristalRoxoController.instance.myDelegateAppear();
        }

        

    }
    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(speed * horizontal, 0);

        movement *= Time.deltaTime;

        transform.Translate(movement);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground" || collision.gameObject.tag == "CristalRoxo")
        {
            ground = true;
        }
        
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
}
