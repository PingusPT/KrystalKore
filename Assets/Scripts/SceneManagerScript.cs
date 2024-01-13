using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SceneManagerScript : MonoBehaviour
{

    public static SceneManagerScript instance;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadMenuScene();
        }
    }

    public string ActualSceneName()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name;
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNewGameScene()
    {
        GameManagerScript.instance.PlayerWantNewGame(true);
        SceneManager.LoadScene(1);
    }

    public void LoadExistingGameScene()
    {
        string path = Application.persistentDataPath + "/player.brocode";

        if (File.Exists(path))
        {
            GameManagerScript.instance.PlayerWantNewGame(false);
            SceneManager.LoadScene(1);

        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
