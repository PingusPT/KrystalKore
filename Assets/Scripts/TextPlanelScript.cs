
using UnityEngine;

public class TextPlanelScript : MonoBehaviour
{
    [SerializeField] GameObject Placa;
    public float fadeSpeed = 5f;

     SpriteRenderer objectRenderer;
     bool isPlayerInside = false;

    void Start()
    {
        objectRenderer = Placa.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
        float targetOpacity = isPlayerInside ? 1f : 0f;
        float currentOpacity = Mathf.Lerp(objectRenderer.color.a, targetOpacity, fadeSpeed * Time.deltaTime);

        
        objectRenderer.color = new Color(objectRenderer.color.r, objectRenderer.color.g, objectRenderer.color.b, currentOpacity);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerInside = false;
        }
    }
}
