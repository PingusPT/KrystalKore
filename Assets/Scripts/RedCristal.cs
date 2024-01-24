using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCristal : MonoBehaviour
{
    Vector3 point;

    Animator anim;

    List<GameObject> arrayDestructibleObjects = new List<GameObject>();

    bool exploded = false;

    AudioSource src;
    [SerializeField] AudioClip heartBeat, explode;

    
    void Start()
    {
        src = gameObject.GetComponent<AudioSource>();
        src.loop = true;
        src.clip = heartBeat;
        src.PlayDelayed(1f);
    }

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        point = gameObject.transform.localPosition;
    }

    public void TurnOfff()
    {
        RedCristalSpawner.instance.StartRespawnTime(gameObject,point,anim, src, heartBeat);
        gameObject.SetActive(false);
        exploded = false;
    }

    public void StartExplo()
    {
        
        anim.SetTrigger("Ativar");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "CoisasDestrutiveis" && collision.gameObject.name != "Break1") || collision.gameObject.tag == "EngrenagemDestrutivel" || collision.gameObject.tag == "Player")
        {
            
            arrayDestructibleObjects.Add(collision.gameObject);

        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "CoisasDestrutiveis" && collision.gameObject.name != "Break1") || collision.gameObject.tag == "EngrenagemDestrutivel" || collision.gameObject.tag == "Player")
        {
            
            arrayDestructibleObjects.Remove(collision.gameObject);

        }
    }

    private void Sound()
    {
        if (src != null && explode != null)
        {
            src.Stop();
            src.clip = explode;
            src.loop = false;
            src.Play();

        }
        else
        {
            Debug.LogError("erro neste Game Object" + gameObject.name);
        }
    }

    private void Explode()
    {
        
        if (!exploded)
        {
           
            foreach (GameObject obj in arrayDestructibleObjects.ToArray())
            {
                if (obj.gameObject.tag == "CoisasDestrutiveis" && obj.gameObject.name != "Break1")
                {
                    
                    obj.GetComponent<Animator>().SetFloat("speed", 1f);
                    StartCoroutine(DelayDestroi(obj));
                    
                    GameManagerScript.instance.ObjectDestroid(obj.gameObject);

                }
                if (obj.gameObject.tag == "EngrenagemDestrutivel")
                {
                    obj.gameObject.GetComponent<AberturasExplo>().Ativacao();
                }
                else if (obj.gameObject.tag == "Player")
                {
                    GameManagerScript.instance.lifeScript.TakeDamage();
                }
            }


            exploded = true;
        }
    }

    public IEnumerator DelayDestroi(GameObject obj)
    {
        yield return new WaitForSeconds(0.7f);
        obj.SetActive(false);
    }
}
