using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject whatToSpawn;

    [SerializeField] private Sprite spritesToChooseFrom;

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < 25; x++)
        {
            for (int y = 0; y < 25; y++)
            {
                Instantiate(whatToSpawn, new Vector2(x, y), Quaternion.identity);
            }
        }
    }
}
