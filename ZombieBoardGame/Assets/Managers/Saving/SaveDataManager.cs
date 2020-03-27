using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
            {
                File.Delete(Application.persistentDataPath + "/gamesave.save");
            }
        }
    }

    public void saveGame()
    {
        //create save data objects
        SaveData saveData = new SaveData();
        //save map
        saveData.savedMap = saveMap(saveData);
        //save actions
        saveData.savedAction = saveActions(saveData);
        //save misc
        MainBase myBase = MainBase.instance;
        saveData.colonyName = myBase.colonyName;
        saveData.numSurvivors = myBase.numberOfPeopleInBase;
        saveData.numWeapons = myBase.numberOfWeaponsInBase;
        saveData.turnNumber = TurnManager.instance.currentTurn;

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
        MainBase myBase = MainBase.instance;
        myBase.colonyName = saveData.colonyName;
        myBase.numberOfPeopleInBase = saveData.numSurvivors;
        myBase.numberOfWeaponsInBase = saveData.numWeapons;
        TurnManager.instance.currentTurn = saveData.turnNumber;
    }

    private SaveTile[,] saveMap(SaveData data)
    {
        data.savedMap = new SaveTile[Map.instance.mapSize, Map.instance.mapSize];

        for (int x = 0; x < Map.instance.mapSize; x++)
        {
            for (int y = 0; y < Map.instance.mapSize; y++)
            {
                data.savedMap[x, y] = Map.instance.MapList[x, y].createSaveOfTile();
            }
        }
        return data.savedMap;
    }

    private SaveAction[] saveActions(SaveData data)
    {
        return ActionList.instance.saveToFile();
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
