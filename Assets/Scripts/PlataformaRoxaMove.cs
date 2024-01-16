using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaRoxaMove : MonoBehaviour
{
    BoxCollider2D RoxoCollider; // tem

    public Transform pointA; // tem
    public Transform pointB;// tem

    public bool Diferent = false;// tem
    public bool Vertical = true;// tem

    bool flag = true;

    Vector3 currentTarget;// tem

    public GameObject barrier;// tem

    public float speed = 2f;// tem
    public float distanceStopFromBarrier = 5f;// tem

    void Start()
    {

        RoxoCollider = gameObject.GetComponent<BoxCollider2D>();
        currentTarget = Diferent ? pointB.position : pointA.position;


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
            // Parte diferente do com barreira

            if (RoxoCollider.enabled)
            {
                // se o box collider tiver ativo

                if (Vertical)
                {


                    if (transform.position.y > barrier.transform.position.y)
                    {

                        if (transform.position.y <= barrier.transform.position.y + distanceStopFromBarrier)
                        {


                            currentTarget = pointB.position;


                        }
                        if (transform.position == pointB.position)
                        {

                            currentTarget.y = barrier.transform.position.y + distanceStopFromBarrier;
                        }



                    }
                    else
                    {
                        //Esta PARTE FUNCIONA--------------------------------------------------------------------
                        

                            if (transform.position.y >= barrier.transform.position.y - distanceStopFromBarrier)
                            {
                                currentTarget = pointA.position;

                            }

                            if (transform.position == pointA.position)
                            {

                                currentTarget.y = barrier.transform.position.y - distanceStopFromBarrier;
                            }
                       
                            
                        
                    }
                }
                else
                {
                    if (transform.position.x > barrier.transform.position.x)
                    {

                        if (transform.position.x <= barrier.transform.position.x + distanceStopFromBarrier)
                        {


                            currentTarget = pointB.position;


                        }
                        if (transform.position == pointB.position)
                        {

                            currentTarget.x = barrier.transform.position.x + distanceStopFromBarrier;
                        }



                    }
                    else
                    {
                        //Esta PARTE FUNCIONA--------------------------------------------------------------------


                        if (transform.position.x >= barrier.transform.position.x - distanceStopFromBarrier)
                        {

                            currentTarget = pointA.position;

                        }

                        if (transform.position == pointA.position)
                        {
                            currentTarget.x = barrier.transform.position.x - distanceStopFromBarrier;
                        }
    

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
        if (Vertical)
        {
            if (currentTarget.y > pointA.position.y && currentTarget != pointA.position)
            {

                currentTarget = pointB.position;
            }
            else
            {
                currentTarget = pointA.position;
            }
        }
        else
        {
            if (currentTarget.x > pointA.position.x && currentTarget != pointA.position)
            {

                currentTarget = pointB.position;
            }
            else
            {
                currentTarget = pointA.position;
            }
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
