using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TileWeightConfig")]
public class TileWeight : ScriptableObject
{
    public float suburbWeight, officeWeight, schoolWeight, policeStationWeight, hospitalWeight; 
}
