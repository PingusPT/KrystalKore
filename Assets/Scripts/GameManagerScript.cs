using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManagerScript : MonoBehaviour
{

    public static GameManagerScript instance;


    GameObject Player;
    GameObject legss;
    GameObject purplePower;
    GameObject redArm;

    LifeManager lifeScript;
    PlayerMovement moveScript;
    ColorAura auraScript;

    GameObject[] wallsDestroid; // Falta Salvar isto------------------------------------------------------------------------------

    bool PlayerSeted = false;
    bool newGame = true;
    string path;

    //Caso de temppo fazer qual dos estados os cristais roxos estao e os azuis tmb



    // Start is called before the first frame update
    void Start()
    {
        if(!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        //SampleScene
    }

    private void Awake()
    {

       

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManagerScript.instance.ActualSceneName() == "Joao" && !PlayerSeted)// Trocar para SampleScene dps TA A FAZER NO UPDATE E TA SEMPRE A IR BUSCAR
        {

            GetComponents();

        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SavePlayer();
        }
    }


    public void SavePlayer()
    {
        SaveScript.instance.SaveBinaryGame(moveScript, lifeScript, auraScript);
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

    

    public void ResetSavedGame()
    {

    }


    public void GetComponents()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        legss = GameObject.FindGameObjectWithTag("Legs");
        redArm = GameObject.FindGameObjectWithTag("BracoVermelho");
        purplePower = GameObject.FindGameObjectWithTag("BracoRoxo");

        wallsDestroid = GameObject.FindGameObjectsWithTag("CoisasDestrutiveis"); // Unity allawys get by name declining Break3, Break2, Break1, HavetoBreak


        lifeScript = Player.GetComponent<LifeManager>();
        moveScript = Player.GetComponent<PlayerMovement>();
        auraScript = Player.GetComponentInChildren<ColorAura>();
        //auraScript = Player.GetComponent<ColorAura>(); //// NAO ESTAS A PEGAR O SCRIPT DO CHILD tens de pegar do child  

        path = Application.persistentDataPath + "/player.brocode";

        if (File.Exists(path) && !newGame)
        {
            GameManagerScript.instance.SetPlayer(SaveScript.instance.GetBinarySave());
            PlayerSeted = true;
        }
    }

    public void PlayerWantNewGame(bool setIfIsNewGame)
    {
        newGame = setIfIsNewGame;

        if(newGame)
        {
            path = null;
        }
    }
}
