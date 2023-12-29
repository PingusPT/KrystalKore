using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class LifeManager : MonoBehaviour
{
   [SerializeField] GameObject[] vidas;

    int life;

    // Start is called before the first frame update
    void Start()
    {
        
        life = vidas.Length -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        switch(life)
        {
            case 0:
                {

                    vidas[life].SetActive(false);
                    life--;
                    return;
                }
            case 1:
                {
                    vidas[life].SetActive(false);
                    life--;
                    return;
                }
            case 2:
                {
                    vidas[life].SetActive(false);
                    life--;
                    return;
                }
            case 3:
                {
                    vidas[life].SetActive(false);
                    life--;
                    return;
                }
        }

        
    }
}
