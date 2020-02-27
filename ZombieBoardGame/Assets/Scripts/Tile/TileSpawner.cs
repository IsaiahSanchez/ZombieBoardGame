using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileSpawner : MonoBehaviour
{
    [SerializeField] private int mapSize = 10;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject boundary;

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
                //create
                TileMain temp = Instantiate(tilePrefab, new Vector2(transform.position.x + indexX, transform.position.y + indexY), Quaternion.identity).GetComponent<TileMain>();

                //setup
                TileType thisType = pickTileType();


                //initialize and save to map
                int rand = Random.Range(0, 5);
                temp.init(indexX, indexY, thisType, TypeInformations[rand].tileSprite);
                Map.instance.addTileAt(indexX, indexY, temp);
            }
        }
        spawnBoundaries();
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

    public TileType pickTileType()
    {
        TileType temp = TileType.None;
        //random probabilities based on each type of tile.


        return temp;
    }
}
