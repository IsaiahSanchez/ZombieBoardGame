using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionList : MonoBehaviour
{
    public static ActionList instance;

    [SerializeField] private TextMeshProUGUI Title, MissionTypeText, ChanceOfSuccess, Content, peopleChange, weaponsChange, successText;

    private Action currentAction = null;
    private int numPeopleChange = 0;
    private int numWeaponsChange = 0;
    private bool lastMissionWasSuccessful = false;

    private List<Action> listOfActions = new List<Action>();
    private List<Action> removeList = new List<Action>();
    private Queue<Action> tempActionQueue = new Queue<Action>();

    private void Awake()
    {
        instance = this;
    }

    public SaveAction[] saveToFile()
    {
        SaveAction[] actions = new SaveAction[listOfActions.Count];

        for (int index = 0; index < actions.Length; index++)
        {
            SaveAction tempAction = new SaveAction();
            tempAction.tileXCoord = listOfActions[index].tileXCoord;
            tempAction.tileYCoord = listOfActions[index].tileYCoord;
            tempAction.numberOfTurnsRemaining = listOfActions[index].numberOfTurnsRemaining;
            tempAction.numberOfPeopleSent = listOfActions[index].numberOfPeopleSent;
            tempAction.numberOfWeaponsSent = listOfActions[index].numberOfWeaponsSent;
            tempAction.missionType = listOfActions[index].missionType;

            actions[index] = tempAction;
        }

        return actions;
    }

    public void loadFromFile(SaveAction[] actionsSaved)
    {
        if (actionsSaved != null)
        {
            for (int index = 0; index < actionsSaved.Length; index++)
            {
                Action currentSavedAction = new Action();
                currentSavedAction.tileXCoord = actionsSaved[index].tileXCoord;
                currentSavedAction.tileYCoord = actionsSaved[index].tileYCoord;
                currentSavedAction.numberOfTurnsRemaining = actionsSaved[index].numberOfTurnsRemaining;
                currentSavedAction.numberOfPeopleSent = actionsSaved[index].numberOfPeopleSent;
                currentSavedAction.numberOfWeaponsSent = actionsSaved[index].numberOfWeaponsSent;
                currentSavedAction.missionType = actionsSaved[index].missionType;

                addAction(currentSavedAction);
                Map.instance.getTileAt(currentSavedAction.tileXCoord, currentSavedAction.tileYCoord).updateMissionActiveGraphic(true);
            }
        }
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
            AudioManager.instance.playSound("page", new Vector2(0, 0));
            //transfer the people and weapons over to the main base
            MainBase.instance.numberOfPeopleInBase += numPeopleChange;
            MainBase.instance.numberOfWeaponsInBase += numWeaponsChange;
            MainBase.instance.numberOfPeopleInBase += currentAction.numberOfPeopleSent;
            MainBase.instance.numberOfWeaponsInBase += currentAction.numberOfWeaponsSent;

            TileMain thisTile = Map.instance.getTileAt(currentAction.tileXCoord, currentAction.tileYCoord);
            thisTile.numberOfSurvivors -= numPeopleChange;
            thisTile.numberOfWeapons -= numWeaponsChange;
            thisTile.hasMissionActiveCurrently = false;

            if (lastMissionWasSuccessful == true)
            {
                if (currentAction.missionType == MissionType.scout)
                {
                    thisTile.hasBeenScouted = true;
                }

                if (currentAction.missionType == MissionType.raid)
                {
                    thisTile.numberOfZombiesOccupying = 0;
                }

                if (currentAction.missionType == MissionType.settle)
                {
                    thisTile.isPartOfColony = true;
                    Map.instance.refreshAllTiles();
                }
            }
            currentAction = null;
        }

        if (tempActionQueue.Count > 0)
        {
            StartCoroutine(flashTitle());

            numWeaponsChange = 0;
            numPeopleChange = 0;

            //dequeue an object and look at it
            currentAction = tempActionQueue.Dequeue();

            if (currentAction.missionType == MissionType.scout)
            {
                successText.text = "The " + Map.instance.getTileAt(currentAction.tileXCoord, currentAction.tileYCoord).tileType.ToString() + " was scouted!";
            }

            if (currentAction.missionType == MissionType.raid)
            {
                successText.text = "All zombies in this " + Map.instance.getTileAt(currentAction.tileXCoord, currentAction.tileYCoord).tileType.ToString() + " were eliminated!";
            }

            if (currentAction.missionType == MissionType.settle)
            {
                successText.text = "The " + Map.instance.getTileAt(currentAction.tileXCoord, currentAction.tileYCoord).tileType.ToString() + " was settled!";
            }

            //Do maths based on the mission type to figure out how it went.
            //sets chances of success based off mission type
            float chanceOfSuccessValue = determineChanceOfSuccess(currentAction);
            //calculates whether or not it is a success based on the mission success chance that was given
            float rand = Random.Range(0, 1f);
            //populate the correct data fields based on the type of mission (make sure you take in the fields at the top of this object???)
            Title.text = "Morning";
            MissionTypeText.text = "" + UppercaseFirst(currentAction.missionType.ToString()) + " : " + Map.instance.getTileAt(currentAction.tileXCoord, currentAction.tileYCoord).tileType;
            ChanceOfSuccess.text = "The chance of success on this mission was : " + (int)(chanceOfSuccessValue * 100f) + "%";

            if (rand < chanceOfSuccessValue)
            {
                Content.text = "Mission was a success!";
                lastMissionWasSuccessful = true;
                if ((Map.instance.getTileAt(currentAction.tileXCoord, currentAction.tileYCoord).numberOfSurvivors > 0 && currentAction.missionType == MissionType.raid) == true || (Map.instance.getTileAt(currentAction.tileXCoord, currentAction.tileYCoord).numberOfSurvivors > 0 && currentAction.missionType == MissionType.settle) == true)
                {
                    numPeopleChange = Map.instance.getTileAt(currentAction.tileXCoord, currentAction.tileYCoord).numberOfSurvivors;
                }

                if ((Map.instance.getTileAt(currentAction.tileXCoord, currentAction.tileYCoord).numberOfWeapons > 0 && currentAction.missionType == MissionType.raid) == true || (Map.instance.getTileAt(currentAction.tileXCoord, currentAction.tileYCoord).numberOfWeapons > 0 && currentAction.missionType == MissionType.settle) == true) 
                {
                    numWeaponsChange = Map.instance.getTileAt(currentAction.tileXCoord, currentAction.tileYCoord).numberOfWeapons;
                }
                //update the current tiles picture to show that it is not in a mission anymore
                Map.instance.getTileAt(currentAction.tileXCoord, currentAction.tileYCoord).updateMissionActiveGraphic(false);
            }
            else
            {
                CameraShake.instance.addShake(.25f, .25f, .25f, .25f);
                Content.text = "Mission was a failure! There were great losses.";
                lastMissionWasSuccessful = false;
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
                successText.text = "";
                //update the current tiles picture to show that it is not in a mission anymore
                Map.instance.getTileAt(currentAction.tileXCoord, currentAction.tileYCoord).updateMissionActiveGraphic(false);
            }

            if (Mathf.Abs(numPeopleChange) > 0)
            {
                peopleChange.text = "Change in people : " + numPeopleChange;
            }
            else
            {
                peopleChange.text = "";
            }

            if (Mathf.Abs(numWeaponsChange) > 0)
            {
                weaponsChange.text = "Change in weapons : " + numWeaponsChange;
            }
            else
            {
                weaponsChange.text = "";
            }

            

            

            
        }
        else
        {
            //show defaults
            MainActionChoicePanel.instance.closeCurrentPanel();
            TurnManager.instance.advanceDay();
        }

        Map.instance.checkIfMapIsOwnedByPlayer();
    }

    private IEnumerator flashTitle()
    {
        MissionTypeText.color = new Color(1f, 1f, 1f);
        yield return new WaitForSeconds(.1f);
        MissionTypeText.color = new Color(0, 0, 0);
    }

    private string UppercaseFirst(string s)
    {
        // Check for empty string.
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        // Return char and concat substring.
        return char.ToUpper(s[0]) + s.Substring(1);
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
