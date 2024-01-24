using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{

    [SerializeField] GameObject MenuPause;

    [SerializeField] AudioClip PauseIntro; // Talvez mais um para o Clique

    AudioSource src;

    bool pause = false;

    // Start is called before the first frame update
    void Start()
    {
        MenuPause.SetActive(pause);
        src = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {

            pause = !pause;
            
            
            Pause(pause);
            
        }

    }

    public void Pause(bool wantToPause)
    {
        Debug.Log("Pause");
        MenuPause.SetActive(wantToPause);
        src.PlayOneShot(PauseIntro);
        Time.timeScale = (wantToPause) ? 0 : 1;
    }

    public void SaveWorld()
    {
        GameManagerScript.instance.SavePlayer();
        Debug.Log("WorldSabed");
    }
}
