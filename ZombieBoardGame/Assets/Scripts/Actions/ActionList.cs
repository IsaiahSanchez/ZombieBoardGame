using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionList : MonoBehaviour
{
    public static ActionList instance;

    private List<Action> listOfActions = new List<Action>();
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

    private void checkEachActionForCompletion()
    {
        foreach (Action action in listOfActions)
        {
            //go through the prompts
            if (action.numberOfTurnsRemaining < 0)
            {
                tempActionQueue.Enqueue(action);
                listOfActions.Remove(action);
            }
        }
    }

    public void cycleThroughActionsThatHappened()
    {
        if (tempActionQueue.Count > 0)
        {
            //dequeue an object and look at it
            //populate the correct data fields based on the type of mission (make sure you take in the fields at the top of this object???)
            //wait for the button Press
        }
        else
        {
            //show defaults
        }
    }
}
