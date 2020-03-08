﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionList : MonoBehaviour
{
    public static ActionList instance;

    [SerializeField] private TextMeshProUGUI Title, MissionTypeText, ChanceOfSuccess, Content, peopleChange, weaponsChange;

    private Action currentAction = null;
    private int numPeopleChange = 0;
    private int numWeaponsChange = 0;

    private List<Action> listOfActions = new List<Action>();
    private List<Action> removeList = new List<Action>();
    private Queue<Action> tempActionQueue = new Queue<Action>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //load actions from save if they exist
    }

    private void loadFromFile()
    {

    }

    private void saveToFile()
    {

    }

    public void addAction(Action actionToAdd)
    {
        listOfActions.Add(actionToAdd);
    }

    public void endTurn()
    {
        foreach(Action action in listOfActions)
        {
            action.numberOfTurnsRemaining--;
        }
    }

    public void checkEachActionForCompletion()
    {
        foreach (Action action in listOfActions)
        {
            //go through the prompts
            if (action.numberOfTurnsRemaining <= 0)
            {
                tempActionQueue.Enqueue(action);
                removeList.Add(action);
            }
        }
        foreach (Action action in removeList)
        {
            listOfActions.Remove(action);
        }
        removeList.Clear();
        cycleThroughActionsThatHappened();
    }

    public void cycleThroughActionsThatHappened()
    {
        if (currentAction != null)
        {
            //transfer the people and weapons over to the main base
            currentAction = null;
        }

        if (tempActionQueue.Count > 0)
        {
            numWeaponsChange = 0;
            numPeopleChange = 0;

            //dequeue an object and look at it
            currentAction = tempActionQueue.Dequeue();

            //Do maths based on the mission type to figure out how it went.
            //sets chances of success based off mission type
            float chanceOfSuccessValue = determineChanceOfSuccess(currentAction);
            //calculates whether or not it is a success based on the mission success chance that was given
            float rand = Random.Range(0, 1f);
            //populate the correct data fields based on the type of mission (make sure you take in the fields at the top of this object???)
            Title.text = "Morning";
            MissionTypeText.text = "" + currentAction.missionType.ToString() + " : " + Map.instance.getTileAt(currentAction.tileXCoord, currentAction.tileYCoord).tileType;
            ChanceOfSuccess.text = "The chance of success on this mission was : " + (chanceOfSuccessValue * 100f) + "%";

            if (rand < chanceOfSuccessValue)
            {
                Content.text = "Mission was a success!";
                if (Map.instance.getTileAt(currentAction.tileXCoord, currentAction.tileYCoord).numberOfSurvivors > 0)
                {
                    numPeopleChange = Map.instance.getTileAt(currentAction.tileXCoord, currentAction.tileYCoord).numberOfSurvivors;
                }

                if (Map.instance.getTileAt(currentAction.tileXCoord, currentAction.tileYCoord).numberOfWeapons > 0)
                {
                    numWeaponsChange = Map.instance.getTileAt(currentAction.tileXCoord, currentAction.tileYCoord).numberOfWeapons;
                }
            }
            else
            {
                Content.text = "Mission was a failure! There were great losses.";
                numPeopleChange = (int)((1f - chanceOfSuccessValue) * currentAction.numberOfPeopleSent)*-1;
                if (numPeopleChange <= 0)
                {
                    numPeopleChange = -1;
                }

                if (currentAction.numberOfWeaponsSent > 0)
                {
                    float randWepChance = Random.Range(.1f, .8f);
                    numWeaponsChange = (int)(randWepChance * currentAction.numberOfWeaponsSent)*-1;
                    if (numWeaponsChange <= 0)
                    {
                        numWeaponsChange = -1;
                    }
                }
            }

            peopleChange.text = "Change in people : " + numPeopleChange;
            weaponsChange.text = "Change in weapons : " + numWeaponsChange;
        }
        else
        {
            //show defaults
            MainActionChoicePanel.instance.closeCurrentPanel();
            TurnManager.instance.advanceDay();
        }
    }

    private float determineChanceOfSuccess(Action thisAction)
    {
        TileMain tempTile = Map.instance.getTileAt(thisAction.tileXCoord, thisAction.tileYCoord);
        switch (thisAction.missionType)
        {
            case MissionType.scout:
                return scoutChanceMath(tempTile);

            case MissionType.raid:
                return scoutChanceMath(tempTile);

            case MissionType.settle:
                if (thisAction.numberOfWeaponsSent > 0)
                {
                    return 1f;
                }
                else
                {
                    return .8f;
                }
        }
        return 0;
    }

    public float scoutChanceMath(TileMain tile)
    {
        int numberOfArmedPeople = currentAction.numberOfPeopleSent - (currentAction.numberOfPeopleSent - currentAction.numberOfWeaponsSent);
        if (numberOfArmedPeople > currentAction.numberOfPeopleSent)
        {
            numberOfArmedPeople = currentAction.numberOfPeopleSent;
        }
        float temp = ((currentAction.numberOfPeopleSent * PeopleCapabilities.numZombiesOnePersonCanHandle)
            + (numberOfArmedPeople * (PeopleCapabilities.weaponKillModifier))) / tile.numberOfZombiesOccupying;
        return Mathf.Clamp(temp, 0, 1f);
    }
}
