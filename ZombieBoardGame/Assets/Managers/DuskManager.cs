using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DuskManager : MonoBehaviour
{
    public static DuskManager instance;

    [SerializeField] private TextMeshProUGUI peopleDefendingText, wallsToDefendText, chanceText;
    [SerializeField] private float difficultyScale = 1;

    public float currentChance = 0f;
    private void Awake()
    {
        instance = this;
    }

    public void init(int peopleDef, int wallsToCover)
    {
        peopleDefendingText.text = "People defending the walls : " + peopleDef;
        wallsToDefendText.text = "Walls to cover : " + wallsToCover;

        if (((float)peopleDef / difficultyScale) >= wallsToCover)
        {
            currentChance = .95f;
        }
        else
        {
            currentChance = ((float)peopleDef / difficultyScale)/ wallsToCover;
        }

        chanceText.text = "Chance of success is " + (currentChance*100) + "%";
    }

}
