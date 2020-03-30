using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPanel : MonoBehaviour
{
    public static ActionPanel instance;
    public int currentXCoord, currentYCoord;

    [SerializeField] private ActionSubPanel mySubPanel;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        
    }

    public void fillPanelData()
    {
        mySubPanel.enable();
    }

    public void startAction(MissionType typeOfMission, int numberOfPeople, int numberOfWeapons)
    {
        //create a temp action object
        Action action = new Action();
        action.tileXCoord = currentXCoord;
        action.tileYCoord = currentYCoord;

        int tempTurnAmount = 0;
        switch (Map.instance.MapList[currentXCoord, currentYCoord].tileType)
        {
            case TileType.Suburbs:

                switch (typeOfMission)
                {
                    case MissionType.scout:
                        tempTurnAmount = 1;
                        break;
                    case MissionType.raid:
                        tempTurnAmount = 2;
                        break;
                    case MissionType.settle:
                        //maybe make this scale down based on how many walls need built
                        tempTurnAmount = checkNumberOfWallsForTile(currentXCoord, currentYCoord);
                        break;
                }

                break;
            case TileType.School:
                switch (typeOfMission)
                {
                    case MissionType.scout:
                        tempTurnAmount = 2;
                        break;
                    case MissionType.raid:
                        tempTurnAmount = 4;
                        break;
                    case MissionType.settle:
                        tempTurnAmount = checkNumberOfWallsForTile(currentXCoord, currentYCoord);
                        break;
                }
                break;
            case TileType.Offices:

                switch (typeOfMission)
                {
                    case MissionType.scout:
                        tempTurnAmount = 2;
                        break;
                    case MissionType.raid:
                        tempTurnAmount = 3;
                        break;
                    case MissionType.settle:
                        //maybe make this scale down based on how many walls need built
                        tempTurnAmount = checkNumberOfWallsForTile(currentXCoord, currentYCoord);
                        break;
                }
                
                break;
            case TileType.Hospital:

                switch (typeOfMission)
                {
                    case MissionType.scout:
                        tempTurnAmount = 2;
                        break;
                    case MissionType.raid:
                        tempTurnAmount = 3;
                        break;
                    case MissionType.settle:
                        //maybe make this scale down based on how many walls need built
                        tempTurnAmount = checkNumberOfWallsForTile(currentXCoord, currentYCoord);
                        break;
                }

                break;
            case TileType.PoliceStation:

                switch (typeOfMission)
                {
                    case MissionType.scout:
                        tempTurnAmount = 2;
                        break;
                    case MissionType.raid:
                        tempTurnAmount = 4;
                        break;
                    case MissionType.settle:
                        //maybe make this scale down based on how many walls need built
                        tempTurnAmount = checkNumberOfWallsForTile(currentXCoord, currentYCoord);
                        break;
                }

                break;
            case TileType.None:
                tempTurnAmount = 0;
                break;
        }
        
        action.numberOfTurnsRemaining = tempTurnAmount;
        action.missionType = typeOfMission;

        action.numberOfPeopleSent = numberOfPeople;
        action.numberOfWeaponsSent = numberOfWeapons;

        MainBase.instance.numberOfPeopleInBase -= numberOfPeople;
        MainBase.instance.numberOfWeaponsInBase -= numberOfWeapons;
        //input into the list
        ActionList.instance.addAction(action);
        Map.instance.getTileAt(currentXCoord, currentYCoord).updateMissionActiveGraphic(true);
        MainActionChoicePanel.instance.closeCurrentPanel();
        MainActionChoicePanel.instance.cancelSelection();
        //show some feedback?
    }

    private int checkNumberOfWallsForTile(int x, int y)
    {
        int temp = 4;
        Map theMap = Map.instance;
        TileMain thisTile = theMap.getTileAt(x, y);


        if (y < theMap.mapSize - 1)
        {
            if (theMap.getTileAt(x, y + 1).isPartOfColony)
            {
                temp--;
            }
        }

        if (x < theMap.mapSize - 1)
        {
            if (theMap.getTileAt(x + 1, y).isPartOfColony)
            {
                temp--;
            }
        }

        if (y > 0)
        {
            if (theMap.getTileAt(x, y - 1).isPartOfColony)
            {
                temp--;
            }
        }

        if (x > 0)
        {
            if (theMap.getTileAt(x - 1, y).isPartOfColony)
            {
                temp--;
            }
        }
        

        return temp;
    }
}
