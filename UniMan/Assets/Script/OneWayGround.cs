using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OneWayGround : MonoBehaviour
{
    TilemapCollider2D tilemap;
    CompositeCollider2D composite;
    private void Start()
    {
        tilemap = GetComponent<TilemapCollider2D>();
        composite = GetComponent<CompositeCollider2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            tilemap.isTrigger = true;
            composite.isTrigger = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tilemap.isTrigger = false;
            composite.isTrigger = false;
        }
    }
}
