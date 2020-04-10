using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainActionChoicePanel : MonoBehaviour
{
    public static MainActionChoicePanel instance;

    [SerializeField] private GameObject mainChoicePanel;
    [SerializeField] private ActionPanel scoutPanel;
    [SerializeField] private ActionPanel raidPanel;
    [SerializeField] private ActionPanel settlePanel;
    [SerializeField] private GameObject tempDuskPanel, tempNightPanel, morningPanel;

    [SerializeField] private Button scoutButton, raidButton, settleButton;

    [SerializeField] private TextMeshProUGUI tileTypeText;

    [SerializeField] private GameObject highlightImage;

    private int currentX, currentY;
    private GameObject currentOpenPanel = null;
    private bool aTileIsSelected = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (aTileIsSelected)
        {
            Debug.Log(currentX + " " + currentY);
            //handle showing a selection image on the currently selected tile
            highlightImage.SetActive(true);
            highlightImage.transform.position = Map.instance.getTileAt(currentX, currentY).gameObject.transform.position;
        }
        else
        {
            highlightImage.SetActive(false);
        }
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

            if (Map.instance.getTileAt(xIndex, yIndex).hasBeenScouted == true)
            {
                scoutButton.interactable = false;
                if (Map.instance.getTileAt(xIndex, yIndex).numberOfZombiesOccupying > 0)
                {
                    raidButton.interactable = true;
                    settleButton.interactable = false;
                }
                else
                {
                    settleButton.interactable = true;
                    raidButton.interactable = false;
                }
            }
            else
            {
                scoutButton.interactable = true;
                raidButton.interactable = false;
                settleButton.interactable = false;
            }

            aTileIsSelected = true;
            //check cases for each different situation
            //if havent scouted then you shouldn't be able to click settle and maybe even raid?
            //if you have already scouted then you don't need to again
            //stuff like that

            AudioManager.instance.playSound("page", new Vector2(0, 0));
        }
    }

    private string findOutWhatTileType(int x, int y)
    {
        return Map.instance.getTileAt(x, y).tileType.ToString();
    }

    public void cancelSelection()
    {
        aTileIsSelected = false;
        AudioManager.instance.playSound("page", new Vector2(0, 0));
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
        AudioManager.instance.playSound("page", new Vector2(0, 0));
    }

    public void openDuskPanel()
    {
        tempDuskPanel.SetActive(true);
        currentOpenPanel = tempDuskPanel;
        AudioManager.instance.playSound("page", new Vector2(0, 0));
    }

    public void openNightPanel()
    {
        tempNightPanel.SetActive(true);
        currentOpenPanel = tempNightPanel;
        AudioManager.instance.playSound("page", new Vector2(0, 0));
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
            temp.fillPanelData();
        }
        AudioManager.instance.playSound("knock", new Vector2(0, 0));
    }
}
