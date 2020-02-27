using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Action")]
public class Action : ScriptableObject
{
    public int tileXCoord = 0;
    public int tileYCoord = 0;
    public int numberOfTurnsRemaining = 0;
    public int numberOfPeopleSent = 0;
    public int numberOfWeaponsSent = 0;
    public MissionType missionType = MissionType.none;
}

public enum MissionType {none, scout, raid, settle}
