using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{

    [SerializeField] GameObject MenuPause;

    bool pause = false;

    // Start is called before the first frame update
    void Start()
    {
        MenuPause.SetActive(pause);
        
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
        MenuPause.SetActive(wantToPause);
        Time.timeScale = (wantToPause) ? 0 : 1;
    }

    public void SaveWorld()
    {
        GameManagerScript.instance.SavePlayer();
        
        
    }
}
