using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Map : MonoBehaviour
{
    public static Map instance;

    public TileMain[,] MapList = new TileMain[10,10];

    private void Awake()
    {
        instance = this;
    }

    public TileMain getTileAt(int x, int y)
    {
        return MapList[x, y];
    }

    public void addTileAt(int x, int y, TileMain tileToAdd)
    {
        MapList[x, y] = tileToAdd;
    }

    public void LoadMap()
    {
        Debug.Log("Loaded");
    }

    public void SaveMap()
    {
        Debug.Log("TempSaved");
    }
}
