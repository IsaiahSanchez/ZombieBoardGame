using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveTile
{
    public TileType tileType = TileType.None;
    public int numberOfZombiesOccupying = 0;
    public int numberOfSurvivors = 0;
    public int numberOfWeapons = 0;

    public bool isPartOfColony = false;
    public bool hasBeenScouted = false;
}
