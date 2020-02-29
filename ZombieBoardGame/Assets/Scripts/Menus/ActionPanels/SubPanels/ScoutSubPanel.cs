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
        //update chance by doing math
        
    }

    public override void submitInfoToActionList()
    {
        parent.startAction(MissionType.scout, numPeople, numWeapons);
    }
}
