
using UnityEngine;

public class PlataformScript : MonoBehaviour
{

    Rigidbody2D rgb;

    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManagerScript.instance.moveScript.InPlataform(rgb.velocity);
        }
    }

}
