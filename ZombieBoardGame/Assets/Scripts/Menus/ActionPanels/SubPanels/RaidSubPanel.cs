using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidSubPanel : ActionSubPanel
{

    protected override void updateChance()
    {
        base.updateChance();
    }

    public override void submitInfoToActionList()
    {
        parent.startAction(MissionType.raid, numPeople, numWeapons);
    }
}
