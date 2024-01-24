
using UnityEngine;

public class DetectionDoor : MonoBehaviour
{

    [SerializeField] GameObject Porta;

    [SerializeField] AudioClip PortaAbre, PortaFecha;

    AudioSource src;
    
    Animator anim;

    bool flag = true;
    bool flag1 = true;


    float time = 4f;
    bool CountDown = false;

    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        src.loop = false;
        anim = Porta.GetComponent<Animator>();
 
    }

    // Update is called once per frame
    void Update()
    {
       
        if(CountDown)
        {
            
            time -= Time.deltaTime;

            
            if(time < 0)
            {
                GameManagerScript.instance.moveScript.enabled = true;
                CountDown = false;
                time = 4f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && flag)
        {
            
            flag = false;
            if (src != null && PortaFecha != null)
            {
                src.PlayOneShot(PortaFecha);
            }
            else
            {
                Debug.LogError("erro neste Game Object" + gameObject.name);
            }
            anim.SetTrigger("fechar");
            collision.gameObject.GetComponent<Animator>().SetBool("Idle", true);
            GameManagerScript.instance.moveScript.enabled = false;
            CountDown = true;
        }
        if(collision.gameObject.tag == "Chave" && flag1)
        {
            if (src != null && PortaAbre != null)
            {
                src.PlayOneShot(PortaAbre);
            }
            else
            {
                Debug.LogError("erro neste Game Object" + gameObject.name);
            }
            anim.SetTrigger("abrir");
            flag1 = false;

            Destroy(collision.gameObject);
        }
    }

}
