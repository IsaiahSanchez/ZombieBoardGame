using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainActionChoicePanel : MonoBehaviour
{
    public static MainActionChoicePanel instance;

    [SerializeField] private GameObject mainChoicePanel;
    [SerializeField] private ActionPanel scoutPanel;
    [SerializeField] private ActionPanel raidPanel;
    [SerializeField] private ActionPanel settlePanel;
    [SerializeField] private GameObject tempDuskPanel, tempNightPanel, morningPanel;

    [SerializeField] private TextMeshProUGUI tileTypeText;

    private int currentX, currentY;
    private GameObject currentOpenPanel = null;

    private void Awake()
    {
        instance = this;
    }

    public void openMainChoicePanel(int xIndex, int yIndex)
    {
        currentX = xIndex;
        currentY = yIndex;
        if (TurnManager.instance.stateOfTurn == TurnState.Day)
        {
            closeCurrentPanel();
            tileTypeText.text = findOutWhatTileType(xIndex, yIndex);
            mainChoicePanel.SetActive(true);
            currentOpenPanel = mainChoicePanel;

            //check cases for each different situation
            //if havent scouted then you shouldn't be able to click settle and maybe even raid?
            //if you have already scouted then you don't need to again
            //stuff like that
        }
    }

    private string findOutWhatTileType(int x, int y)
    {
        return Map.instance.getTileAt(x, y).tileType.ToString();
    }

    public void closeCurrentPanel()
    {
        if (currentOpenPanel != null)
        {
            currentOpenPanel.SetActive(false);
        }
    }

    public void openMorningPanel()
    {
        morningPanel.SetActive(true);
        currentOpenPanel = morningPanel;
    }

    public void openDuskPanel()
    {
        tempDuskPanel.SetActive(true);
        currentOpenPanel = tempDuskPanel;
    }

    public void openNightPanel()
    {
        tempNightPanel.SetActive(true);
        currentOpenPanel = tempNightPanel;
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
            case 2:
                temp = settlePanel;
                break;
        }

        if (temp != null)
        {
            temp.gameObject.SetActive(true);
            currentOpenPanel = temp.gameObject;
            temp.currentXCoord = currentX;
            temp.currentYCoord = currentY;
        }
    }
}
