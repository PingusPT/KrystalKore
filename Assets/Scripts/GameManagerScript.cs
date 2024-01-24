using System.Collections;

using UnityEngine;
using System.IO;

public class GameManagerScript : MonoBehaviour
{

    public static GameManagerScript instance;

    GameObject EndGame;
    Animator endGameAnim;
    public GameObject Player;
    GameObject legss;
    GameObject purplePower;
    GameObject redArm;

    public LifeManager lifeScript;
    public PlayerMovement moveScript;
    public ColorAura auraScript;

    GameObject[] wallsDestroid; 
    bool[] breakedWall = { true, true, true, true, true };

    bool PlayerSeted = false;
    
    bool newGame = true;
    string path;

    public float timeBetweenSaves = 600f;
    float time;

    



    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 100;

        if(!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        path = Application.persistentDataPath + "/player.brocode";
        time = timeBetweenSaves;
        DontDestroyOnLoad(gameObject);
        //SampleScene
    }

    private void Awake()
    {

       

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManagerScript.instance.ActualSceneName() == "Joao")// Trocar para SampleScene dps TA A FAZER NO UPDATE E TA SEMPRE A IR BUSCAR
        {
            time -= Time.deltaTime;
           

            if(time < 0)
            {
                
                time = timeBetweenSaves;
                SavePlayer();
            }

            if(!PlayerSeted)
            {
                
                GetComponents();
                
            }
           

        }

        if (SceneManagerScript.instance.ActualSceneName() == "Menu")
        {
            PlayerSeted = false;
            
        }

    }


    public void SavePlayer()
    {
        SaveScript.instance.SaveBinaryPlayer(moveScript, lifeScript, auraScript);
        SaveScript.instance.SaveBinaryWorld(breakedWall);
    }

    public void SetPlayer(PlayerData data)
    {
        // ta a tentar dar Set sem estar na scene correta e ainda nao tem os components
        moveScript.SetPlayerMovementPropreties(data.legs, data.position[0], data.position[1]);
        lifeScript.SetLifeManagerProperties(data.health, data.lastSpawnPoint[0], data.lastSpawnPoint[1]);
        auraScript.SetColorAuraProperties(data.RedArm, data.PurplePower);

        if(data.PurplePower)
        {
            purplePower.SetActive(false);
        }
        if(data.RedArm)
        {
            redArm.SetActive(false);
        }
        if(data.legs)
        {
            legss.SetActive(false);
        }
        
    }

    public void SetWorld(bool[] data)
    {
        int inv = wallsDestroid.Length - 1;

        for (int i = 0; i < wallsDestroid.Length; i++)
        {
            wallsDestroid[i].SetActive(data[inv]);
            inv--;
        }

        
    }
    
    public bool isPlayerSeted()
    {
        return PlayerSeted;
    }

    public PlayerMovement ReturnPlayer()
    {
        return moveScript;
    }


    public void GetComponents()
    {
        PlayerSeted = true;
        EndGame = GameObject.FindGameObjectWithTag("EndGame");
        Player = GameObject.FindGameObjectWithTag("Player");
        endGameAnim = EndGame.GetComponent<Animator>();
        legss = GameObject.FindGameObjectWithTag("Legs");
        redArm = GameObject.FindGameObjectWithTag("BracoVermelho");
        purplePower = GameObject.FindGameObjectWithTag("BracoRoxo");

        wallsDestroid = GameObject.FindGameObjectsWithTag("CoisasDestrutiveis"); // Unity allawys get by name declining Break3, Break2, Break1, HavetoBreak
        
        lifeScript = Player.GetComponent<LifeManager>();
        moveScript = Player.GetComponent<PlayerMovement>();
        auraScript = Player.GetComponentInChildren<ColorAura>();
        //auraScript = Player.GetComponent<ColorAura>(); //// NAO ESTAS A PEGAR O SCRIPT DO CHILD tens de pegar do child  

        EndGame.SetActive(true);
        Time.timeScale = 1;

        if (File.Exists(path) && !newGame)
        {
            
            SetPlayer(SaveScript.instance.GetBinaryPlayerSave());
            SetWorld(SaveScript.instance.GetBinaryWorldSave());
            
        }
        
    }

    public void PlayerWantNewGame(bool setIfIsNewGame)
    {
        newGame = setIfIsNewGame;

        if(newGame)
        {
            File.Delete(path);
        }
    }

    public void ObjectDestroid(GameObject objectDestroid)
    {

        int inv = wallsDestroid.Length - 1;

        for (int i = 0; i < wallsDestroid.Length; i++)
        {
            if(wallsDestroid[i].name == objectDestroid.name)
            {
                breakedWall[inv] = false;
            }

            inv--;
        }
    }

    public void PlayerCach(GameObject player)
    {
        StartCoroutine(PlayerLegs(player));
    }

    public IEnumerator PlayerLegs(GameObject player)
    {
        Vector2 scale = player.transform.localScale;
        moveScript.enabled = false;
        yield return new WaitForSeconds(0.2f);
        player.transform.position = new Vector2(player.transform.position.x - 0.5f, player.transform.position.y + 1f);
        player.transform.localScale = new Vector2(0.5138532f, 0.5138532f);
        yield return new WaitForSeconds(3.38f);
        player.transform.localScale = scale;
        moveScript.enabled = true;
    }

    public void DoEndAnimation()
    {
        endGameAnim.SetTrigger("End");
    }

   

    
}
