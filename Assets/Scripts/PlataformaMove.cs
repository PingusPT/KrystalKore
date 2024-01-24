
using UnityEngine;

public class PlataformaMove : MonoBehaviour
{
    public Transform pontoEsquerda;
    public Transform pontoDireita;

    float FixedDelay;
    public float DelayTime = 3.45f;

    bool jaPode = false;
    public float velocidade = 5f;

    private bool movendoParaDireita = true;

    private void Start()
    {
        FixedDelay = DelayTime;
        if (gameObject.tag == "PlataformaMovel")
        {
            jaPode = true;
        }
    }
    void Update()
    {
       

    }

    private void FixedUpdate()
    {
       

        if (gameObject.tag == "PlataformaMovel")
        {
            if (transform.position.x <= pontoEsquerda.position.x)
            {
                if (jaPode == false && DelayTime == FixedDelay)
                {
                    
                    jaPode = true;
                }

            }

            else if (transform.position.x >= pontoDireita.position.x)
            {
                movendoParaDireita = false;
            }
        }
        else
        {
            if (transform.position.y <= pontoEsquerda.position.y)
            {
                movendoParaDireita = true;
            }
            else if (transform.position.y >= pontoDireita.position.y)
            {
                if (jaPode == false && DelayTime == FixedDelay)
                {
                    jaPode = true;
                }
            }
        }

        

        if (jaPode)
        {
            DelayTime -= Time.deltaTime;

            if (DelayTime < 0)
            {
                jaPode = false;
                DelayTime = FixedDelay;

                if (gameObject.tag == "PlataformaMovel")
                {
                    movendoParaDireita = true;
                }
                else
                {
                    movendoParaDireita = false;
                }

            }


        }



        if (!jaPode)
        {
            if (gameObject.tag == "PlataformaMovel")
            {
                if (movendoParaDireita)
                {
                    
                    transform.Translate(Vector2.right * velocidade * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector2.left * velocidade * Time.deltaTime);
                }
            }
            else
            {

                if (movendoParaDireita)
                {
                    transform.Translate(Vector2.right * velocidade * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector2.left * velocidade * Time.deltaTime);
                }

            }
        }
        
        
    }
}
