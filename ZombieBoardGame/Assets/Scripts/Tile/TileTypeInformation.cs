using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(menuName = "TileTypeInfo")]
public class TileTypeInformation : ScriptableObject
{
    public TileType tileType;
    public Sprite tileSprite;
    public float chanceOfZombies;
    public int maximumNumberOfZombies;
    public float chanceOfSurvivors;
    public int maxNumberOfSurvivors;
    public float chanceOfWeapons;
    public int maxNumberOfWeapons;
}
