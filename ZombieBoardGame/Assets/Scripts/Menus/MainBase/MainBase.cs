using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainBase : MonoBehaviour
{
    public static MainBase instance;

    public string colonyName = "";
    public int numberOfPeopleInBase = 1;
    public int numberOfWeaponsInBase = 1;
    public int numberOfWalls = 4;

    private void Awake()
    {
        instance = this;
    }

    
}
