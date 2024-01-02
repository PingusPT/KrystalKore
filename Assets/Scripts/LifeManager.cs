using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LifeManager : MonoBehaviour
{
    [SerializeField] GameObject[] vidas;
    

    Vector2 LastCheckPoint;

    int life;

    public float DefaultTimeInvencible = 2f;
    float timeInvencible = 0;
    bool invencible = false;


    // Start is called before the first frame update
    void Start()
    {
        LastCheckPoint = gameObject.transform.position;
        timeInvencible = DefaultTimeInvencible;
        life = vidas.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            
            TakeDamage();
        }
        if (invencible)
        {
            timeInvencible -= Time.deltaTime;

            if (timeInvencible < 0)
            {
                invencible = false;
                timeInvencible = DefaultTimeInvencible;
            }
        }
        Debug.Log(life + " - " + invencible);
    }

    public void TakeDamage()
    {
        
        if(!invencible)
        {
            invencible = true;
            switch (life)
            {
                case 0:
                    {

                        vidas[life].SetActive(false);
                        
                        life--;
                        Respawn();
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

    public void UpdateCheckPoint(Vector2 check)
    {
        if (check != LastCheckPoint)
        {
            LastCheckPoint = check;
        }

    }

    private void Respawn()
    {
        gameObject.transform.position = LastCheckPoint;
        ResetLifes();
    }

    private void ResetLifes()
    {
        life = vidas.Length - 1;

        for (int i = 0; i < vidas.Length; i++)
        {
            vidas[i].SetActive(true);
        }
    }
}
