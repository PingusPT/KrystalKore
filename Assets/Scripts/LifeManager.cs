
using UnityEngine;


public class LifeManager : MonoBehaviour
{
    [SerializeField] GameObject[] vidas;

    Animator anim;

    PlayerMovement player;

    public  Vector2 LastCheckPoint;

    public int life = 4;

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
        if(GameManagerScript.instance.isPlayerSeted() && !player)
        {
            player = GameManagerScript.instance.ReturnPlayer();
            anim = player.GetComponent<Animator>();
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
                        player.DroppGrabed();
                        anim.SetTrigger("Die");
                        return;
                    }
                case 1:
                    {
                        vidas[life].SetActive(false);
                        anim.SetTrigger("Damage");
                        life--;
                        return;
                    }
                case 2:
                    {
                        anim.SetTrigger("Damage");
                        vidas[life].SetActive(false);
                        life--;
                        return;
                    }
                case 3:
                    {
                        vidas[life].SetActive(false);
                        anim.SetTrigger("Damage");
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

    public void SetLifes()
    {
        /*
        if(life <= 3)
        {
            vidas[3].SetActive(false);
        }

        if (life < 2)
        {
            vidas[2].SetActive(false);
        }
        if (life < 1)
        {
            vidas[1].SetActive(false);
        }
        if (life < 0)
        {
            vidas[0].SetActive(false);
        }
        */
        for (int i = 0; i <= vidas.Length - 1; i++)
        {
            vidas[i].SetActive(life >= i);
        }

    }

    public void SetLifeManagerProperties(int Life, float SpawnX, float SpawnY)
    {
        life = Life;

        SetLifes();

        LastCheckPoint = new Vector2(SpawnX, SpawnY);
    }
}
