using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] private Transform whatToFollow;
    void Update()
    {
        transform.position = new Vector3(whatToFollow.position.x, whatToFollow.position.y, -10);
    }
}
