using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Map : MonoBehaviour
{
    public static Map instance;

    public TileMain[,] MapList;

    public int mapSize = 10;

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

                MapList[x, y].UpdateTileLook();
            }
        }
    }
}
