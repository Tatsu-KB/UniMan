using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject player;
    Vector2 direction;
    public float Speed;
    public int Attack;
    StageManeger maneger;

    // Start is called before the first frame update
    void Awake()
    {
        transform.localScale = transform.localScale;
        if (!player) player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            Vector2 targetPos = player.transform.position;

            float x = targetPos.x;
            float y = targetPos.y;

            rb = GetComponent<Rigidbody2D>();
            maneger = GameObject.FindGameObjectWithTag("StageManeger").GetComponent<StageManeger>();

            direction = new Vector2(x - transform.position.x, y - transform.position.y).normalized;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * Speed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy")
        {
            Destroy(gameObject);
        }
        if(collision.tag == "Player")
        {
            maneger.PlayerDamege(Attack);
        }
        
        if(collision.tag != "Enemy" && collision.tag != "Player")
        {
            maneger.BreakEffect(transform);
        }
    }
}
