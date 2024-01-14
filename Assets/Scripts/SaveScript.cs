using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveScript : MonoBehaviour
{

    static public SaveScript instance;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)// Let only one time of this gameobject exist
        {
            instance = this;
        }
        else
        {

            Destroy(gameObject);

        }

        DontDestroyOnLoad(gameObject);
    }

    public void SaveBinaryPlayer(PlayerMovement playerMove, LifeManager playerLife, ColorAura playerAura) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.brocode";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(playerMove, playerLife, playerAura);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public void SaveBinaryWorld(bool[] WallsDestroid)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/world.brocode";
        FileStream stream = new FileStream(path, FileMode.Create);

        

        formatter.Serialize(stream, WallsDestroid);
        stream.Close();
    }


    public PlayerData GetBinaryPlayerSave()//if the file exists save the data in form of int, close the strean and return the data
    {
        string path = Application.persistentDataPath + "/player.brocode";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }


    public bool[] GetBinaryWorldSave()//if the file exists save the data in form of int, close the strean and return the data
    {
        string path = Application.persistentDataPath + "/world.brocode";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

           bool[] data = formatter.Deserialize(stream) as bool[];

            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
}
