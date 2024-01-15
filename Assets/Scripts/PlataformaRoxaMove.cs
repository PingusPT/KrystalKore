using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaRoxaMove : MonoBehaviour
{
    BoxCollider2D RoxoCollider;

    public Transform pointA; 
    public Transform pointB;

    public bool Diferent = false;

    bool flag = true;

    Vector3 currentTarget;

    public GameObject barrier;

    public float speed = 2f;

    void Start()
    {

        RoxoCollider = gameObject.GetComponent<BoxCollider2D>();
        if(!Diferent)
        {
            currentTarget = pointA.position;
        }
        else
        {
            currentTarget = pointB.position;
        }


    }

    void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        
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
            
            if (RoxoCollider.enabled)
            {
                // se o box collider tiver ativo


                if(transform.position.y > barrier.transform.position.y)
                {

                    if (transform.position.y <= barrier.transform.position.y + 5f)
                    {
                        

                        currentTarget = pointB.position;

                        
                    }
                    if (transform.position == pointB.position)
                    {
                       
                        currentTarget.y = barrier.transform.position.y + 5f;
                    }
                    

                    
                }
                else
                {
                    //Esta PARTE FUNCIONA--------------------------------------------------------------------

                    if (transform.position.y >= barrier.transform.position.y - 5f)
                    {
                       
                            
                        currentTarget = pointA.position;

                        //currentTarget.y = (currentTarget.y == pointA.position.y) ? pointA.position.y : barrier.transform.position.y - 0.2f;

                    }

                    if (transform.position == pointA.position)
                    {
                       
                        currentTarget.y = barrier.transform.position.y - 5f;
                    }
                }

                flag = true;

            }
            else
            {
                if(flag)
                {
                    SetTarget();
                }

                if (Vector3.Distance(transform.position, currentTarget) < 0.1f)
                {
                    
                    currentTarget = (currentTarget == pointA.position) ? pointB.position : pointA.position;
                }

                
            }
        }
        
    }


    public void SetTarget()
    {
        if(currentTarget.y > pointA.position.y && currentTarget != pointA.position) 
        {

            currentTarget = pointB.position;
        }
        else
        {
            currentTarget = pointA.position;
        }

        flag = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
