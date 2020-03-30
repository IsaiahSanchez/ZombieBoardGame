using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Map : MonoBehaviour
{
    public static Map instance;

    public TileMain[,] MapList;

    public int mapSize = 10;
    [SerializeField]private GameObject playerCamera;
    [SerializeField]private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI winTurnText;  
    private void Awake()
    {
        instance = this;
        MapList = new TileMain[mapSize, mapSize];
    }

    public TileMain getTileAt(int x, int y)
    {
        return MapList[x, y];
    }

    public void addTileAt(int x, int y, TileMain tileToAdd)
    {
        MapList[x, y] = tileToAdd;
    }

    public void refreshAllTiles()
    {
        MainBase.instance.numberOfWalls = 0;
        foreach (TileMain tile in MapList)
        {
            tile.UpdateTileLook();
        }
    }

    public void setPlayerCameraToMiddleMostBaseTile()
    {
        List<TileMain> tempList = new List<TileMain>();
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                if (MapList[x, y].isPartOfColony)
                {
                    tempList.Add(MapList[x, y]);
                }
            }
        }

        float averageX = 0, averageY = 0;
        for(int index = 0; index < tempList.Count; index++)
        {
            averageX += tempList[index].xLocation;
            averageY += tempList[index].yLocation;
        }
        averageX = averageX / tempList.Count;
        averageY = averageY / tempList.Count;

        Vector2 positionToMoveCamera = new Vector2(MapList[0,0].transform.position.x + averageX, MapList[0, 0].transform.position.y + averageY);
        playerCamera.transform.position = positionToMoveCamera;
    }

    public void checkIfMapIsOwnedByPlayer()
    {
        if (allTilesOwned())
        {
            winPanel.SetActive(true);
            winTurnText.text = "It only took : " + TurnManager.instance.currentTurn + " days to win!";
        }
    }

    private bool allTilesOwned()
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                if (getTileAt(x,y).isPartOfColony == false)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void LoadMap(SaveTile[,] tilesSaved)
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                MapList[x, y].tileType = tilesSaved[x, y].tileType;
                MapList[x, y].numberOfZombiesOccupying = tilesSaved[x, y].numberOfZombiesOccupying;
                MapList[x, y].numberOfSurvivors = tilesSaved[x, y].numberOfSurvivors;
                MapList[x, y].numberOfWeapons = tilesSaved[x, y].numberOfWeapons;
                MapList[x, y].isPartOfColony = tilesSaved[x, y].isPartOfColony;
                MapList[x, y].hasBeenScouted = tilesSaved[x, y].hasBeenScouted;

                MapList[x, y].updateGraphic();
            }
        }
        Map.instance.refreshAllTiles();
    }
}
