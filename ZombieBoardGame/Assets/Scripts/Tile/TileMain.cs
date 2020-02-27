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

    private void setSprite(Sprite graphicToUse)
    {
        mySprite.sprite = graphicToUse;
    }

    public void UpdateTileLook()
    {




        //Adding and removing walls as needed
        currentNumberOfWalls = 0;
        if (hasNorthWall)
        {
            currentNumberOfWalls++;
            northWall.SetActive(true);
        }
        else
        {
            northWall.SetActive(false);
        }

        if (hasEastWall)
        {
            currentNumberOfWalls++;
            eastWall.SetActive(true);
        }
        else
        {
            eastWall.SetActive(false);
        }

        if (hasSouthWall)
        {
            currentNumberOfWalls++;
            southWall.SetActive(true);
        }
        else
        {
            southWall.SetActive(false);
        }

        if (hasWestWall)
        {
            currentNumberOfWalls++;
            westWall.SetActive(true);
        }
        else
        {
            westWall.SetActive(false);
        }

        //checking fog of war
        fogOfWar.SetActive(false);

    }

    private void OnMouseDown()
    {
        //select this tile
        Debug.Log("" + xLocation + " , " + yLocation);
    }
}

public enum TileType {Suburbs, Offices, PoliceStation, Hospital, None};
