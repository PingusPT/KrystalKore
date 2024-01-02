using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCristalSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject redCristal;
    [SerializeField] GameObject Point;
    public static RedCristalSpawner instance;

    

    void Start()
    {
        instance = this;
    }



    private IEnumerator Respawn(GameObject redCristal, Vector2 Spawn, Animator anim)
    {
        yield return new WaitForSeconds(6);

        redCristal.SetActive(true);
        redCristal.transform.position = Spawn;
        anim.Play("Explode", 0, 0);
        anim.SetTrigger("Desativar");
    }

    public void StartRespawnTime(GameObject redCristal, Vector2 Spawn, Animator anim)
    {
        StartCoroutine(Respawn(redCristal, Spawn, anim));
    }
}
