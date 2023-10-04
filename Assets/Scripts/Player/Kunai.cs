using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    GameObject player;
    Rigidbody2D myRigi;
    public float kunaiSpeed;
    private void Awake()
    {
        player = GameObject.Find("Player");
        myRigi = GetComponent<Rigidbody2D>();
        if (player.transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            myRigi.AddForce(Vector2.left * kunaiSpeed, ForceMode2D.Impulse);
        }
        else
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            myRigi.AddForce(Vector2.right * kunaiSpeed, ForceMode2D.Impulse);
        }

        Destroy(this.gameObject, 5.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.tag != "StopPoint" && other.tag != "Item") {
            Destroy(this.gameObject);
        }
    }
}
