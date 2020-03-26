using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NightManager : MonoBehaviour
{
    public static NightManager instance;

    [SerializeField] private TextMeshProUGUI resultsText, peopleLostText;
    [SerializeField] private float difficultyMod = 1f;

    private void Awake()
    {
        instance = this;
    }

    public void showResults()
    {
        float chance = Random.Range(0, 1f);

        if (chance <= DuskManager.instance.currentChance)
        {
            resultsText.text = "Your colony survived this night...";
            peopleLostText.text = "";
        }
        else
        {
            resultsText.text = "Your colony had a breach what a disaster!";
            //roll people lost with some sort of chance mech?
            int numPeopleLost = (int)Random.Range(1, (MainBase.instance.numberOfWalls / 2) * difficultyMod);

            if (numPeopleLost >= MainBase.instance.numberOfPeopleInBase)
            {
                //gameover
                peopleLostText.text = "You lost " + numPeopleLost + " people in the night!";
                MainBase.instance.numberOfPeopleInBase -= numPeopleLost;

                TurnManager.instance.gameOver();
            }
            else
            {
                peopleLostText.text = "You lost " + numPeopleLost + " people in the night!";
                MainBase.instance.numberOfPeopleInBase -= numPeopleLost;
            }
        }
    }


}
