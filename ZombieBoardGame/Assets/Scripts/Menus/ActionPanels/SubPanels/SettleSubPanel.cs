using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettleSubPanel : ActionSubPanel
{

    protected override void updateChance()
    {
        if (numWeapons > 0)
        {
            ChanceText.text = "" + 100 + "%";
        }
        else
        {
            ChanceText.text = "" + 80 + "%";
        }
    }

    public override void submitInfoToActionList()
    {
        Map.instance.getTileAt(parent.currentXCoord, parent.currentYCoord).hasMissionActiveCurrently = true;
        parent.startAction(MissionType.settle, numPeople, numWeapons);
    }
}
