using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainActionChoicePanel : MonoBehaviour
{
    public static MainActionChoicePanel instance;

    [SerializeField] private GameObject mainChoicePanel;
    [SerializeField] private ActionPanel scoutPanel;
    [SerializeField] private ActionPanel raidPanel;
    [SerializeField] private ActionPanel settlePanel;

    private GameObject currentOpenPanel = null;

    private void Awake()
    {
        instance = this;
    }

    private void openMainChoicePanel()
    {
        closeCurrentPanel();
        mainChoicePanel.SetActive(true);
        currentOpenPanel = mainChoicePanel;
    }

    private void closeCurrentPanel()
    {
        if (currentOpenPanel != null)
        {
            currentOpenPanel.SetActive(false);
        }
    }

    public void openPanel(int panelIndex)
    {
        ActionPanel temp = null;

        closeCurrentPanel();
        switch (panelIndex)
        {
            case 0:
                temp = scoutPanel;
                break;
            case 1:
                temp = raidPanel;
                break;
            case 3:
                temp = settlePanel;
                break;
        }
        if (temp != null)
        {
            temp.gameObject.SetActive(true);
            currentOpenPanel = temp.gameObject;
        }
    }
}
