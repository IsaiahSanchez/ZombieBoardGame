using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionSubPanel : MonoBehaviour
{
    protected int numPeople = 1, numWeapons = 0;
    protected float currentSuccessChance;
    protected ActionPanel parent;
    protected int currentXLoc = 0, currentYLoc = 0;

    [SerializeField] protected TextMeshProUGUI ChanceText, numPeopleText, numWeaponsText, numZombiesText;

    private void Awake()
    {
        parent = GetComponentInParent<ActionPanel>();
    }

    public virtual void enable()
    {
        numPeople = 1;
        numWeapons = 0;
        numPeopleText.text = numPeople.ToString();
        numWeaponsText.text = numWeapons.ToString();
        currentSuccessChance = 0;
        currentXLoc = parent.currentXCoord;
        currentYLoc = parent.currentYCoord;

        updateChance();
    }

    public virtual void tryAddNumPeople(int amtToChange)
    {
        numPeople += amtToChange;
        if (numPeople < 1)
        {
            numPeople += (amtToChange * -1);
        }
        else if (numPeople > MainBase.instance.numberOfPeopleInBase)
        {
            numPeople = MainBase.instance.numberOfPeopleInBase;
        }
        //do the check to see if we are trying to send more people than we have.

        //update numPeople
        numPeopleText.text = numPeople.ToString();
        updateChance();
        AudioManager.instance.playSound("knock", new Vector2(0, 0));
    }

    public virtual void tryAddNumWeapons(int amtToChange)
    {
        numWeapons += amtToChange;
        if (numWeapons < 0)
        {
            numWeapons += (amtToChange * -1);
        }
        else if (numWeapons > MainBase.instance.numberOfWeaponsInBase)
        {
            numWeapons = MainBase.instance.numberOfWeaponsInBase;
        }
        //do the check to see if we are trying to send more weapons than we have.

        //update numWeapon
        numWeaponsText.text = numWeapons.ToString();
        updateChance();
        AudioManager.instance.playSound("knock", new Vector2(0, 0));
    }

    protected virtual void updateChance()
    {
        //update chance by doing math

    }

    public virtual void submitInfoToActionList()
    {
            Map.instance.getTileAt(parent.currentXCoord, parent.currentYCoord).hasMissionActiveCurrently = true;
            parent.startAction(MissionType.none, numPeople, numWeapons);   
    }
}
