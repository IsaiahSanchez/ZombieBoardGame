using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidSubPanel : ActionSubPanel
{

    public override void enable()
    {
        base.enable();
        numZombiesText.text = Map.instance.getTileAt(currentXLoc, currentYLoc).numberOfZombiesOccupying.ToString();
    }

    protected override void updateChance()
    {
        int numberOfArmedPeople = numPeople - (numPeople - numWeapons);
        if (numberOfArmedPeople > numPeople)
        {
            numberOfArmedPeople = numPeople;
        }
        float temp = ((numPeople * PeopleCapabilities.numZombiesOnePersonCanHandle)
            + (numberOfArmedPeople * (PeopleCapabilities.weaponKillModifier))) / Map.instance.getTileAt(currentXLoc, currentYLoc).numberOfZombiesOccupying;
        ChanceText.text = "" + (int)(Mathf.Clamp(temp, 0, 1f) * 100) + "%";
    }

    public override void submitInfoToActionList()
    {
        Map.instance.getTileAt(parent.currentXCoord, parent.currentYCoord).hasMissionActiveCurrently = true;
        parent.startAction(MissionType.raid, numPeople, numWeapons);
    }
}
