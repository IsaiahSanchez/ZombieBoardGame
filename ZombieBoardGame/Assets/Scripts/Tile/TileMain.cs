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
    [SerializeField] private GameObject northWall, eastWall, southWall, westWall, fogOfWar, missionInProgressGraphic;

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

    public void updateMissionActiveGraphic(bool shouldBeActive)
    {
        if (shouldBeActive)
        {
            missionInProgressGraphic.SetActive(true);
        }
        else
        {
            missionInProgressGraphic.SetActive(false);
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

            if (yLocation < currentMap.mapSize - 1)
            {
                if (!currentMap.getTileAt(xLocation, yLocation + 1).isPartOfColony)
                {
                    currentNumberOfWalls++;
                    northWall.SetActive(true);
                }
            }
            else
            {
                currentNumberOfWalls++;
                northWall.SetActive(true);
            }

            if (xLocation < currentMap.mapSize - 1)
            {
                if (!currentMap.getTileAt(xLocation + 1, yLocation).isPartOfColony)
                {
                    currentNumberOfWalls++;
                    eastWall.SetActive(true);
                }
            }
            else
            {
                currentNumberOfWalls++;
                eastWall.SetActive(true);
            }

            if (yLocation > 0)
            {
                if (!currentMap.getTileAt(xLocation, yLocation - 1).isPartOfColony)
                {
                    currentNumberOfWalls++;
                    southWall.SetActive(true);
                }
            }
            else
            {
                currentNumberOfWalls++;
                southWall.SetActive(true);
            }

            if (xLocation > 0)
            {
                if (!currentMap.getTileAt(xLocation - 1, yLocation).isPartOfColony)
                {
                    currentNumberOfWalls++;
                    westWall.SetActive(true);
                }
            }
            else
            {
                currentNumberOfWalls++;
                westWall.SetActive(true);
            }
        }

        if (currentNumberOfWalls > 0)
        {
            MainBase.instance.numberOfWalls += currentNumberOfWalls;
        }

        //checking fog of war
        fogOfWar.SetActive(true);

        StartCoroutine(checkAndSetFogOfWarNearBase());
    }

    public void updateGraphic()
    {
        switch (tileType)
        {
            case TileType.Suburbs:
                setSprite(TileSpawner.instance.TypeInformations[0].tileSprite);
                break;
            case TileType.School:
                setSprite(TileSpawner.instance.TypeInformations[1].tileSprite);
                break;
            case TileType.PoliceStation:
                setSprite(TileSpawner.instance.TypeInformations[2].tileSprite);
                break;
            case TileType.Offices:
                setSprite(TileSpawner.instance.TypeInformations[3].tileSprite);
                break;
            case TileType.Hospital:
                setSprite(TileSpawner.instance.TypeInformations[4].tileSprite);
                break;

        }
    }

    private IEnumerator checkAndSetFogOfWarNearBase()
    {
        yield return new WaitForEndOfFrame();
        if (isPartOfColony)
        {
            fogOfWar.SetActive(false);
            Map currentMap = Map.instance;


            if(xLocation > 0)
            {
                if (yLocation > 0)
                {
                    currentMap.getTileAt(xLocation - 1, yLocation - 1).fogOfWar.SetActive(false);
                }

                if (yLocation < currentMap.mapSize - 1)
                {
                    currentMap.getTileAt(xLocation - 1, yLocation + 1).fogOfWar.SetActive(false);
                }

                currentMap.getTileAt(xLocation - 1, yLocation).fogOfWar.SetActive(false);
            }

            if (xLocation < currentMap.mapSize - 1)
            {
                if (yLocation > 0)
                {
                    currentMap.getTileAt(xLocation + 1, yLocation - 1).fogOfWar.SetActive(false);
                }

                if (yLocation < currentMap.mapSize - 1)
                {
                    currentMap.getTileAt(xLocation + 1, yLocation + 1).fogOfWar.SetActive(false);
                }
                    
                currentMap.getTileAt(xLocation + 1, yLocation).fogOfWar.SetActive(false);
            }

            if (yLocation > 0)
            {
                currentMap.getTileAt(xLocation, yLocation - 1).fogOfWar.SetActive(false);
            }

            if (yLocation < currentMap.mapSize - 1)
            {
                currentMap.getTileAt(xLocation, yLocation + 1).fogOfWar.SetActive(false);
            }
        }
    }


    public void openChoicePanel()
    {
        MainActionChoicePanel.instance.openMainChoicePanel(xLocation, yLocation);
    }

    public SaveTile createSaveOfTile()
    {
        SaveTile temp = new SaveTile();

        temp.tileType = tileType;
        temp.numberOfZombiesOccupying = numberOfZombiesOccupying;
        temp.numberOfSurvivors = numberOfSurvivors;
        temp.numberOfWeapons = numberOfWeapons;
        temp.isPartOfColony = isPartOfColony;
        temp.hasBeenScouted = hasBeenScouted;

        return temp;
    }

    //private void OnMouseUp()
    //{
    //    //select this tile
    //    MainActionChoicePanel.instance.openMainChoicePanel(xLocation, yLocation);
    //    Debug.Log("" + xLocation + " , " + yLocation);
    //}
}

public enum TileType {Suburbs, School, PoliceStation, Offices, Hospital, None};
