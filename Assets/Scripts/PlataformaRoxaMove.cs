using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaRoxaMove : MonoBehaviour
{
    BoxCollider2D RoxoCollider; // tem
    CapsuleCollider2D capsule;
    Rigidbody2D rb;

    public Transform pointA; // tem
    public Transform pointB;// tem

    public bool Diferent = false;// tem
    public bool Vertical = true;// tem

    bool flag = true;

    Vector3 currentTarget;// tem
    Vector3 LastTarget;
    Vector3 direction;

    //[SerializeField] Transform pai;
    
    public bool Scaled = false;

    public GameObject barrier;// tem
    
    public float speed = 50f;// tem
    public float distanceStopFromBarrier = 5f;// tem

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        RoxoCollider = gameObject.GetComponent<BoxCollider2D>();
        capsule = gameObject.GetComponent<CapsuleCollider2D>();
        currentTarget = Diferent ? pointA.position : pointB.position;
        LastTarget = currentTarget;
        if (Diferent && RoxoCollider.enabled)
        {
            Debug.Log("AAAAAAAZ" + gameObject.name);
            currentTarget = barrier.transform.position;
        }

        
        CalculateDirection();
       // pai = gameObject.GetComponentInParent<Transform>();
        
    }

    void Update()
    {

        

        if (!Diferent)
        {

            // Verifica se a plataforma atingiu o alvo atual
            if (Vector3.Distance(transform.position, currentTarget) < 0.1f)
            {
                // Inverte o alvo entre A e B
                currentTarget = (currentTarget == pointA.position) ? pointB.position : pointA.position;
            }
        }
        else
        {
            // Parte diferente do com barreira

            if (RoxoCollider.enabled)
            {
                capsule.enabled = true;

                if (Vector3.Distance(transform.position, barrier.transform.position) < distanceStopFromBarrier)
                {
                    currentTarget = LastTarget;
                }

                if (Vector3.Distance(transform.position, currentTarget) < 0.2f)
                {
                    currentTarget = barrier.transform.position;
                }
                

            }
            else
            {
                capsule.enabled = false;

                if (currentTarget == transform.position)
                {
                    currentTarget = LastTarget;
                }

                if (Vector3.Distance(transform.position, currentTarget) < 0.1f)
                {
                    
                    currentTarget = (currentTarget == pointA.position) ? pointB.position : pointA.position;
                    LastTarget = currentTarget;
                }

                
            }
        }


        CalculateDirection();
        rb.velocity = direction * speed * Time.deltaTime;
    }

    private void CalculateDirection()
    {
        direction = (currentTarget - transform.position).normalized;
        direction.Normalize();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManagerScript.instance.moveScript.InPlataform(rb.velocity);
        }
    }


    
}
