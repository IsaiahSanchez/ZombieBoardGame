using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoutSubPanel : ActionSubPanel
{
    //goal of this class is to exist on the scout panel and do math to update the different info based on what information the player gives
    //It will also take input from the different buttons to change the amounts
    
    protected override void updateChance()
    {
        int numberOfArmedPeople = numPeople - (numPeople - numWeapons);
        if (numberOfArmedPeople > numPeople)
        {
            numberOfArmedPeople = numPeople;
        }
        float temp = ((numPeople * PeopleCapabilities.numZombiesOnePersonCanHandle)
            + (numberOfArmedPeople * (PeopleCapabilities.weaponKillModifier))) / Map.instance.getTileAt(currentXLoc, currentYLoc).numberOfZombiesOccupying;
        float randomdirection = Random.Range(-1f, 1f);
        temp += randomdirection * .10f;
        if (Mathf.Clamp(temp, 0, 1f) == 1f)
        {
            temp -= .12f;
        }
        ChanceText.text = "" + (int)(Mathf.Clamp(temp, 0, 1f) * 100) + "%";
    }

    public override void submitInfoToActionList()
    {
        if (numPeople <= MainBase.instance.numberOfPeopleInBase)
        {
            Map.instance.getTileAt(parent.currentXCoord, parent.currentYCoord).hasMissionActiveCurrently = true;
            parent.startAction(MissionType.scout, numPeople, numWeapons);
        }
        else
        {
            CameraShake.instance.addShake(.1f, .1f, .1f, .2f);
        }
    }
}
