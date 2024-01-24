
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SceneManagerScript : MonoBehaviour
{

    public static SceneManagerScript instance;

    // Start is called before the first frame update

    bool newGame = false;

    void Start()
    {
        
    }
    private void Awake()
    {
        instance = this;
    }

   

    public string ActualSceneName()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name;
    }
    public bool NeedNewGame()
    {
        return newGame;
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNewGameScene()
    {
        GameManagerScript.instance.PlayerWantNewGame(true);
        newGame = true;
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

    public void LoadNewScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    
}
