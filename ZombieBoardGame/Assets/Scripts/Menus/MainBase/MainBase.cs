using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainBase : MonoBehaviour
{
    public static MainBase instance;

    [SerializeField] private TextMeshProUGUI NameOfColony, PeopleText, WeaponsText, TurnText;

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
        NameOfColony.text = colonyName;
        PeopleText.text = numberOfPeopleInBase.ToString();
        WeaponsText.text = numberOfWeaponsInBase.ToString();
        TurnText.text = TurnManager.instance.currentTurn.ToString();
    }
}
