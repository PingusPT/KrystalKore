using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Legs : MonoBehaviour
{
    [SerializeField] AudioClip LegsSound, ArmSound, PuprleSound;

    CinemachineVirtualCamera cam;
    Animator camAnim;
    AudioSource src;
    CircleCollider2D circle;
    void Start()
    {
        if(gameObject.tag == "Legs" || gameObject.tag == "BracoVermelho" || gameObject.tag == "BracoRoxo")
        {
            cam = FindAnyObjectByType<CinemachineVirtualCamera>();
            camAnim = cam.GetComponent<Animator>();
            circle = GetComponent<CircleCollider2D>();
        }
        
        src = GetComponent<AudioSource>();
        src.loop = false;
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.tag == "Legs" && circle.enabled)
        {
            camAnim.SetFloat("speed", 1);
            circle.enabled = false;
            if(src != null && LegsSound != null)
            {
                src.PlayOneShot(LegsSound);
            }
            else
            {
                Debug.LogError("erro neste Game Object" + gameObject.name);
            }
            collision.gameObject.GetComponent<PlayerMovement>().CatchLegs();
            Invoke("delaySetActive", 0.5f);
        }
        if(collision.gameObject.tag == "Player" && gameObject.tag == "BracoVermelho" && circle.enabled)
        {
            camAnim.SetFloat("speed", 1);
            circle.enabled = false;
            if (src != null && ArmSound != null)
            {
                src.PlayOneShot(ArmSound);
            }
            else
            {
                Debug.LogError("erro neste Game Object" + gameObject.name);
            }
            collision.gameObject.GetComponentInChildren<ColorAura>().CatchRedArm();
            //collision.gameObject.GetComponentInChildren<Aura>().CatchRedArm();
            Invoke("delaySetActive", 0.5f);
        }
        if(collision.gameObject.tag == "Player" && gameObject.tag == "BracoRoxo" && circle.enabled)
        {
            camAnim.SetFloat("speed", 1);
            circle.enabled = false;
            if (src != null && PuprleSound != null)
            {
                src.PlayOneShot(PuprleSound);
            }
            else
            {
                Debug.LogError("erro neste Game Object" + gameObject.name);
            }
            collision.gameObject.GetComponentInChildren<ColorAura>().CatchPuprlePower();
            //collision.gameObject.GetComponentInChildren<PlayerMovement>().CatchPurpleArm();
            Invoke("delaySetActive", 0.5f);
        }
        if (collision.gameObject.tag == "Player" && gameObject.tag == "ObjectEnd")
        {
            GameManagerScript.instance.DoEndAnimation();
        }
    }

    private void delaySetActive()
    {
        gameObject.SetActive(false);
    }


}
