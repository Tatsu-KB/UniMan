using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokoBullet : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 direction;
    [SerializeField] float SpeedX, SpeedY, Speed, Speed2;
    [SerializeField] int Attack;
    StageManeger maneger;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        maneger = GameObject.FindGameObjectWithTag("StageManeger").GetComponent<StageManeger>();
        Speed2 = Speed / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (SpeedY > 0) Speed = Speed2;
        if (SpeedY < 0) Speed = Speed2;

        rb.velocity = new Vector2(SpeedX * Speed, SpeedY * Speed);
    }

    public void Inst(float x, float y)
    {
        SpeedX = x;
        SpeedY = y;
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
