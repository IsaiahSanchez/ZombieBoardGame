using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPanel : MonoBehaviour
{
    public static ActionPanel instance;

    private void Awake()
    {
        instance = this;
    }

    public void startAction(MissionType typeOfMission)
    {

    }
}
