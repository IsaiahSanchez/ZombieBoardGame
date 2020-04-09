using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    public string name;

    private void Start()
    {
        StartCoroutine(waitToDestroy());
    }

    private IEnumerator waitToDestroy()
    {
        yield return new WaitForSeconds(.65f);
        Destroy(gameObject);
    }
}
