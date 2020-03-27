using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;

    [SerializeField] private TextMeshProUGUI gameOverTurnSurvivedText;
    [SerializeField] private GameObject gameOverPanel;

    public TurnState stateOfTurn = TurnState.Day;
    public int currentTurn = 1;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(waitToStartCurrentTurnSection());
    }

    public void gameOver()
    {
        gameOverPanel.SetActive(true);
        gameOverTurnSurvivedText.text = "You survived until day : " + currentTurn;
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
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
            stateOfTurn = TurnState.Morning;
            currentTurn++;
            //save
            SaveDataManager.instance.saveGame();
        }
        StartCoroutine(waitToStartCurrentTurnSection());
    }

    private IEnumerator waitToStartCurrentTurnSection()
    {
        yield return new WaitForSeconds(0.1f);
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
            DuskManager.instance.init(MainBase.instance.numberOfPeopleInBase, MainBase.instance.numberOfWalls);
        }
        else if (stateOfTurn == TurnState.LateNight)
        {
            //close temp panel for dusk stuff
            MainActionChoicePanel.instance.closeCurrentPanel();
            //open temp panel for late night stuff
            MainActionChoicePanel.instance.openNightPanel();
            NightManager.instance.showResults();
        }
    }
}

public enum TurnState {Morning, Day, Dusk, LateNight}
