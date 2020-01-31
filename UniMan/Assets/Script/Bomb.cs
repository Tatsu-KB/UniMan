using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 direction;
    public int Attack;
    StageManeger maneger;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        maneger = GameObject.FindGameObjectWithTag("StageManeger").GetComponent<StageManeger>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != gameObject.tag)
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Player")
        {
            maneger.PlayerDamege(Attack);
        }

        if (collision.tag != gameObject.tag && collision.tag != "Player")
        {
            maneger.BreakEffect(transform);
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

