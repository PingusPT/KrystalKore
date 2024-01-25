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
    bool[] breakedWall = { true, true, true, true, true }; // After Upgrade to a scalable array :)

    bool PlayerSeted = false;
    
    bool newGame = true;
    string path;

    public float timeBetweenSaves = 600f;
    float time;

    



    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 100; // frame Rate

        if(!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        path = Application.persistentDataPath + "/player.brocode"; // save path
        time = timeBetweenSaves;
        DontDestroyOnLoad(gameObject);
        
    }

   

    // Update is called once per frame
    void Update()
    {
        if (SceneManagerScript.instance.ActualSceneName() == "Joao") // Sim a scene tem o meu nome :)
        {
            time -= Time.deltaTime;
           

            if(time < 0)
            {
                
                time = timeBetweenSaves; //Automatic save periodic tive
                SavePlayer();
            }

            if(!PlayerSeted)
            {
                
                GetComponents(); // Get components from Scene
                
            }
           

        }

        if (SceneManagerScript.instance.ActualSceneName() == "Menu")
        {
            PlayerSeted = false;
            
        }

    }


    public void SavePlayer()
    {
        SaveScript.instance.SaveBinaryPlayer(moveScript, lifeScript, auraScript); // Save Player
        SaveScript.instance.SaveBinaryWorld(breakedWall); // Save World
    }

    public void SetPlayer(PlayerData data) // Set Player from the Save
    {
        
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

    public void SetWorld(bool[] data) // Sets the World from save
    {
        int inv = wallsDestroid.Length - 1;

        for (int i = 0; i < wallsDestroid.Length; i++)
        {
            wallsDestroid[i].SetActive(data[inv]);
            inv--;
        }

        
    }
    
    public bool isPlayerSeted() // if the player was seted 
    {
        return PlayerSeted;
    }

 
    public void GetComponents() // Gets the Components from the Scene, time.scale = 1, if saveExiste and new game wanted set player 
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
        

        EndGame.SetActive(true);
        Time.timeScale = 1;

        if (File.Exists(path) && !newGame)
        {
            
            SetPlayer(SaveScript.instance.GetBinaryPlayerSave());
            SetWorld(SaveScript.instance.GetBinaryWorldSave());
            
        }
        
    }

    public void PlayerWantNewGame(bool setIfIsNewGame) // new game, delete path
    {
        newGame = setIfIsNewGame;

        if(newGame)
        {
            File.Delete(path);
        }
    }

    public void ObjectDestroid(GameObject objectDestroid) // Check wall destroid
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

    public void PlayerCach(GameObject player) // ISTO NAO ERA PARA ESTAR AQUI LEIA A Comment de baixo
    {
        StartCoroutine(PlayerLegs(player));
    }

    public IEnumerator PlayerLegs(GameObject player) // isto ta aqui pq a ana n quis arranjar uma animação, a escala dela, tava toda bugada, eu n tenho de a aturar ent tive de mudar a scale by code, for the sake of the game
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

    public void DoEndAnimation() // End game Animation, Credits etc.
    {
        endGameAnim.SetTrigger("End");
    }


}
