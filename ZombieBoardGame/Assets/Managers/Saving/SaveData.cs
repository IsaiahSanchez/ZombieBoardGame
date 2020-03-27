using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public SaveTile[,] savedMap;
    public SaveAction[] savedAction;

    public string colonyName;
    public int numSurvivors, numWeapons, turnNumber;
}
