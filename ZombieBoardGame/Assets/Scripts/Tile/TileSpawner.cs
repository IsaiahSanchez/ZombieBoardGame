using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileSpawner : MonoBehaviour
{
    [SerializeField] public int mapSize = 10;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject boundary;
    [SerializeField] private TileWeight weights;

    public TileTypeInformation[] TypeInformations = new TileTypeInformation[5];

    void Start()
    {
        createNewMap();
    }

    public void createNewMap()
    {
        for (int indexX = 0; indexX < mapSize; indexX++)
        {
            for (int indexY = 0; indexY < mapSize; indexY++)
            {
                //createOldMapTile(indexX, indexY);
                pickMapTile(indexX , indexY);
            }
        }
        setPlayerSpawnArea();
        spawnBoundaries();
    }

    private void pickMapTile(int x , int y)
    {
        //create
        TileMain temp = Instantiate(tilePrefab, new Vector2(transform.position.x + x, transform.position.y + y), Quaternion.identity).GetComponent<TileMain>();

        float rand = Random.Range(0, weights.hospitalWeight);
        TileTypeInformation chosenType;
        //0,3,1,2,4


        if (rand <= weights.suburbWeight)
        {
            chosenType = TypeInformations[0];
        }
        else if (rand <= weights.officeWeight)
        {
            chosenType = TypeInformations[3];
        }
        else if (rand <= weights.schoolWeight)
        {
            chosenType = TypeInformations[1];
        }
        else if (rand <= weights.policeStationWeight)
        {
            chosenType = TypeInformations[2];
        }
        else if (rand <= weights.hospitalWeight)
        {
            chosenType = TypeInformations[4];
        }
        else
        {
            chosenType = TypeInformations[0];
        }


        temp.init(x, y, chosenType.tileType, chosenType.tileSprite);
        temp.populateTile(calculateSurvivorCount(chosenType),
                            calculateWeaponCount(chosenType),
                            calculateZombieCount(chosenType));

        temp.name = "" + x + "," + y;
        Map.instance.addTileAt(x, y, temp);
    }

    private void createOldMapTile(int indexX, int indexY)
    {
        //create
        TileMain temp = Instantiate(tilePrefab, new Vector2(transform.position.x + indexX, transform.position.y + indexY), Quaternion.identity).GetComponent<TileMain>();

        //setup
        int rand = Random.Range(0, 5);

        temp.init(indexX, indexY, TypeInformations[rand].tileType, TypeInformations[rand].tileSprite);

        //initialize basic data
        temp.init(indexX, indexY, TypeInformations[rand].tileType, TypeInformations[rand].tileSprite);
        //populate tile with the other information
        temp.populateTile(calculateSurvivorCount(TypeInformations[rand]),
                            calculateWeaponCount(TypeInformations[rand]),
                            calculateZombieCount(TypeInformations[rand]));
        //send to map list
        temp.name = "" + indexX + "," + indexY;
        Map.instance.addTileAt(indexX, indexY, temp);
    }

    private int calculateSurvivorCount(TileTypeInformation info)
    {
        float rand = Random.Range(0, 1f);
        if (rand <= info.chanceOfSurvivors)
        {
            return Random.Range(1, info.maxNumberOfSurvivors + 1);
        }
        else
        {
            return 0;
        }
    }

    private int calculateWeaponCount(TileTypeInformation info)
    {
        float rand = Random.Range(0, 1f);
        if (rand <= info.chanceOfWeapons)
        {
            return Random.Range(1, info.maxNumberOfWeapons + 1);
        }
        else
        {
            return 0;
        }
    }

    private int calculateZombieCount(TileTypeInformation info)
    {
        float rand = Random.Range(0, 1f);
        if (rand <= info.chanceOfZombies)
        {
            return Random.Range(1, info.maximumNumberOfZombies + 1);
        }
        else
        {
            return 0;
        }
    }

    private void setPlayerSpawnArea()
    {
        int xRand = Random.Range(2, mapSize - 2);
        int yRand = Random.Range(2, mapSize - 2);

        TileMain startingTile = Map.instance.getTileAt(xRand, yRand);
        startingTile.init(xRand, yRand, TileType.Suburbs, TypeInformations[0].tileSprite);
        startingTile.isPartOfColony = true;
        startingTile.numberOfZombiesOccupying = 0;
        startingTile.numberOfSurvivors = 0;
        startingTile.numberOfWeapons = 0;
        //make camera look at the starting tile
        Map.instance.refreshAllTiles();

        //going to create the 8 tiles around the player as kind of a starter learning experience
    }

    private void spawnBoundaries()
    {
        //create bottom
        Instantiate(boundary, new Vector2(transform.position.x, transform.position.y - 1.25f), Quaternion.identity);
        //top
        Instantiate(boundary, new Vector2(transform.position.x, transform.position.y + mapSize + .25f), Quaternion.identity);
        //left
        Instantiate(boundary, new Vector2(transform.position.x - 1.25f, transform.position.y), Quaternion.Euler(new Vector3(0,0,90)));
        //right
        Instantiate(boundary, new Vector2(transform.position.x + mapSize + .25f, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 90)));
    }

}
