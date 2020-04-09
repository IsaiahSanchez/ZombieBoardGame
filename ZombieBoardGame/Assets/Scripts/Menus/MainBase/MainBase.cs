using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainBase : MonoBehaviour
{
    public static MainBase instance;

    [SerializeField] private TextMeshProUGUI NameOfColony, PeopleText, WeaponsText, TurnText;
    [SerializeField] private GameObject colonyNameTab, tutorialTab;
    [SerializeField] private TextMeshProUGUI inputName;

    public bool isTypingName = false;
    public string colonyName = "";
    public int numberOfPeopleInBase = 4;
    public int numberOfWeaponsInBase = 2;
    public int numberOfWalls = 4;

    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && TurnManager.instance.stateOfTurn == TurnState.Day)
        {
            AudioManager.instance.playSound("page", new Vector2(0, 0));
            MainActionChoicePanel.instance.closeCurrentPanel();
        }

        NameOfColony.text = colonyName;
        PeopleText.text = numberOfPeopleInBase.ToString();
        WeaponsText.text = numberOfWeaponsInBase.ToString();
        TurnText.text = TurnManager.instance.currentTurn.ToString();
    }

    public void displayColonyNameTab()
    {
        isTypingName = true;
        colonyNameTab.SetActive(true);
    }

    public void submitColonyNameChange()
    {
        AudioManager.instance.playSound("knock", new Vector2(0, 0));
        if (inputName != null)
        {
            colonyName = inputName.text;
            colonyNameTab.SetActive(false);
            isTypingName = false;
            tutorialTab.SetActive(true);
        }
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
