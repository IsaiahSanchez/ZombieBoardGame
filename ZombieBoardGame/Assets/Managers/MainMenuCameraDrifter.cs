using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraDrifter : MonoBehaviour
{
    [SerializeField] private float driftSpeed;

    private Rigidbody2D myBody;

    private void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        float x = 0, y = 0;
        x = Random.Range(-1f, 1f);
        y = Random.Range(-1f, 1f);

        myBody.velocity = new Vector2(x, y).normalized * driftSpeed;
    }

    private void ChangeDirection()
    {
        float x = 0, y = 0;
        x = myBody.velocity.y;
        y = myBody.velocity.x * -1f;

        myBody.velocity = new Vector2(x, y).normalized * driftSpeed ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChangeDirection();
    }
}
