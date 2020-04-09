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

    public override void enable()
    {
        base.enable();
        numPeople = 3;
        numPeopleText.text = numPeople.ToString();
        numZombiesText.text = "0";
        updateChance();
    }

    public override void tryAddNumPeople(int amtToChange)
    {
        numPeople += amtToChange;
        if (numPeople < 3)
        {
            numPeople += (amtToChange * -1);
        }
        else if (numPeople > 3)
        {
            numPeople = 3;
        }
        //do the check to see if we are trying to send more people than we have.

        //update numPeople
        numPeopleText.text = numPeople.ToString();
        updateChance();
        AudioManager.instance.playSound("knock", new Vector2(0, 0));
    }

    public override void submitInfoToActionList()
    {
        if (numPeople <= MainBase.instance.numberOfPeopleInBase)
        {
            Map.instance.getTileAt(parent.currentXCoord, parent.currentYCoord).hasMissionActiveCurrently = true;
            parent.startAction(MissionType.settle, numPeople, numWeapons);
        }
        else
        {
            //make error noise and shake screen
            CameraShake.instance.addShake(.1f, .1f, .1f, .25f);
        }
    }

}
