using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionList : MonoBehaviour
{
    public static ActionList instance;

    private List<Action> listOfActions = new List<Action>();

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

    private void checkEachActionForCompletion()
    {
        foreach (Action action in listOfActions)
        {
            //go through the prompts
        }
    }
}
