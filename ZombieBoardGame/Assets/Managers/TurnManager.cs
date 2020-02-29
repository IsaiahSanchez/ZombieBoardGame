using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;

    public TurnState stateOfTurn = TurnState.Day;
    public int currentTurn = 0;

    private void Awake()
    {
        instance = this;
    }

    public void advanceDay()
    {
        if (stateOfTurn != TurnState.LateNight)
        {
            stateOfTurn++;
        }
        else
        {
            stateOfTurn = TurnState.Morning;
            currentTurn++;
            //save
        }
    }
}

public enum TurnState {Morning, Day, Dusk, LateNight}
