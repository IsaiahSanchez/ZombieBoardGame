using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveDataManager : MonoBehaviour
{
    private void Awake()
    {
        
    }



    public void saveGame()
    {
        //create save data objects
        SaveData saveData = new SaveData();
        //save map
        saveMap(saveData);
        //save actions
        saveActions(saveData);
        //save misc


        //finalize save
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, saveData);
        file.Close();

        Debug.Log("Finished Saving Successfully");
    }



    public void loadGame()
    {
        //create save data object
        SaveData saveData = new SaveData();
        //start loading
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
        saveData = (SaveData)bf.Deserialize(file);
        file.Close();


        //load map
        loadMap(saveData);
        //load actions
        loadActions(saveData);
        //load misc

    }



    private void saveMap(SaveData data)
    {
        data.savedMap = new SaveTile[Map.instance.mapSize, Map.instance.mapSize];

        for (int x = 0; x < Map.instance.mapSize; x++)
        {
            for (int y = 0; y < Map.instance.mapSize; y++)
            {
                data.savedMap[x, y] = Map.instance.MapList[x, y].createSaveOfTile();
            }
        }
    }

    private void saveActions(SaveData data)
    {
        data.savedAction = ActionList.instance.saveToFile();
    }


    private void loadMap(SaveData data)
    {
        Map.instance.LoadMap(data.savedMap);
    }

    private void loadActions(SaveData data)
    {
        ActionList.instance.loadFromFile(data.savedAction);
    }
}
