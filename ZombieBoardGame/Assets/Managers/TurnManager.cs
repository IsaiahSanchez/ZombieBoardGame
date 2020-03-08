using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;

    public TurnState stateOfTurn = TurnState.Day;
    public int currentTurn = 1;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        
    }

    public void endDay()
    {
        if (stateOfTurn == TurnState.Day)
        {
            advanceDay();
        }
    }

    public void advanceDay()
    {
        if (stateOfTurn != TurnState.LateNight)
        {
            stateOfTurn++;
        }
        else
        {
            ActionList.instance.endTurn();
            //call save to all save functions
            stateOfTurn = TurnState.Morning;
            currentTurn++;
            //save
        }
        startCurrentTurnSection();
    }

    private void startCurrentTurnSection()
    {
        if (stateOfTurn == TurnState.Morning)
        {
            //close temp panel for late night stuff
            MainActionChoicePanel.instance.closeCurrentPanel();

            //open morning panel and initiate the actionList manager
            MainActionChoicePanel.instance.openMorningPanel();
            ActionList.instance.checkEachActionForCompletion();
        }
        else if (stateOfTurn == TurnState.Day)
        {
            //close morning panel
            MainActionChoicePanel.instance.closeCurrentPanel();
            //dont think there is anything else that needs done for the dayTime. maybe could show animation of turn number here?
        }
        else if (stateOfTurn == TurnState.Dusk)
        {
            MainActionChoicePanel.instance.closeCurrentPanel();
            //open temp panel for the dusk stuff
            MainActionChoicePanel.instance.openDuskPanel();
        }
        else if (stateOfTurn == TurnState.LateNight)
        {
            //close temp panel for dusk stuff
            MainActionChoicePanel.instance.closeCurrentPanel();
            //open temp panel for late night stuff
            MainActionChoicePanel.instance.openNightPanel();
        }
    }
}

public enum TurnState {Morning, Day, Dusk, LateNight}
