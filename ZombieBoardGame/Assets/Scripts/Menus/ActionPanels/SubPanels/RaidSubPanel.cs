using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidSubPanel : ActionSubPanel
{

    protected override void OnEnable()
    {
        numZombiesText.text = Map.instance.getTileAt(currentXLoc, currentYLoc).numberOfZombiesOccupying.ToString();
        base.OnEnable();
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
        ChanceText.text = "" + Mathf.Clamp(temp, 0, 1f) * 100 + "%";
    }

    public override void submitInfoToActionList()
    {
        parent.startAction(MissionType.raid, numPeople, numWeapons);
    }
}
