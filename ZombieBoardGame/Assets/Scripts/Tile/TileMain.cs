using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMain : MonoBehaviour
{
    public int xLocation = 0;
    public int yLocation = 0;

    public TileType tileType = TileType.None;
    public int numberOfZombiesOccupying = 0;
    public int numberOfSurvivors = 0;
    public int numberOfWeapons = 0;
    public bool hasNorthWall, hasEastWall, hasSouthWall, hasWestWall;
    public int currentNumberOfWalls = 0;
    public bool isPartOfColony = false;
    public bool hasBeenScouted = false;
    public bool hasMissionActiveCurrently = false;

    [SerializeField] private SpriteRenderer mySprite;
    [SerializeField] private GameObject northWall, eastWall, southWall, westWall, fogOfWar;

    private void Start()
    {
            
    }

    public void init(int xLoc, int yloc, TileType typeShouldBe, Sprite graphic)
    {
        xLocation = xLoc;
        yLocation = yloc;
        tileType = typeShouldBe;
        setSprite(graphic);
        UpdateTileLook();
    }

    public void populateTile(int numSurvivors, int numWeapons, int numZombies)
    {
        numberOfSurvivors = numSurvivors;
        numberOfWeapons = numWeapons;
        numberOfZombiesOccupying = numZombies;
    }

    private void setSprite(Sprite graphicToUse)
    {
        mySprite.sprite = graphicToUse;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && fogOfWar.activeSelf == false && isPartOfColony == false && hasMissionActiveCurrently == false)
        {
            Vector2 mouseAim = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            if (mouseAim.x < transform.position.x + .4f && mouseAim.x > transform.position.x - .4f 
                && mouseAim.y < transform.position.y + .4f && mouseAim.y > transform.position.y - .4f
                && Input.mousePosition.x < 1460 && Input.mousePosition.y > 170f)
            {
                MainActionChoicePanel.instance.openMainChoicePanel(xLocation, yLocation);
            }
        }
    }

    public void UpdateTileLook()
    {
        //Adding and removing walls as needed
        currentNumberOfWalls = 0;
        northWall.SetActive(false);
        eastWall.SetActive(false);
        southWall.SetActive(false);
        westWall.SetActive(false);


        if (isPartOfColony == true)
        {
            Map currentMap = Map.instance;
            if (!currentMap.getTileAt(xLocation, yLocation + 1).isPartOfColony)
            {
                currentNumberOfWalls++;
                northWall.SetActive(true);
            }

            if (!currentMap.getTileAt(xLocation + 1, yLocation).isPartOfColony)
            {
                currentNumberOfWalls++;
                eastWall.SetActive(true);
            }

            if (!currentMap.getTileAt(xLocation, yLocation - 1).isPartOfColony)
            {
                currentNumberOfWalls++;
                southWall.SetActive(true);
            }

            if (!currentMap.getTileAt(xLocation - 1, yLocation).isPartOfColony)
            {
                currentNumberOfWalls++;
                westWall.SetActive(true);
            }
        }


        //checking fog of war
        fogOfWar.SetActive(true);
        if (isPartOfColony)
        {
            fogOfWar.SetActive(false);
            Map currentMap = Map.instance;
            currentMap.getTileAt(xLocation - 1, yLocation - 1).fogOfWar.SetActive(false);
            currentMap.getTileAt(xLocation, yLocation - 1).fogOfWar.SetActive(false);
            currentMap.getTileAt(xLocation + 1, yLocation - 1).fogOfWar.SetActive(false);
            currentMap.getTileAt(xLocation + 1, yLocation).fogOfWar.SetActive(false);
            currentMap.getTileAt(xLocation + 1, yLocation + 1).fogOfWar.SetActive(false);
            currentMap.getTileAt(xLocation, yLocation + 1).fogOfWar.SetActive(false);
            currentMap.getTileAt(xLocation - 1, yLocation + 1).fogOfWar.SetActive(false);
            currentMap.getTileAt(xLocation - 1, yLocation).fogOfWar.SetActive(false);
        }

    }

    public void openChoicePanel()
    {
        MainActionChoicePanel.instance.openMainChoicePanel(xLocation, yLocation);
    }

    //private void OnMouseUp()
    //{
    //    //select this tile
    //    MainActionChoicePanel.instance.openMainChoicePanel(xLocation, yLocation);
    //    Debug.Log("" + xLocation + " , " + yLocation);
    //}
}

public enum TileType {Suburbs, School, PoliceStation, Offices, Hospital, None};
