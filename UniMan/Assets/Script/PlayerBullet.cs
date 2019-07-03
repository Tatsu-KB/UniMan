using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject player;
    Vector2 direction;
    [SerializeField] int SpeedX, SpeedY,Speed,Speed2;
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
        if (SpeedX > 0 && SpeedY > 0) Speed = Speed2;
        if (SpeedX > 0 && SpeedY < 0) Speed = Speed2;

        rb.velocity = new Vector2(SpeedX * Speed, SpeedY * Speed);
    }
    
    public void Inst(int x,int y)
    {
        SpeedX = x;
        SpeedY = y;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Bee" || collision.tag == "Piranha")
        {
            maneger.GetComponent<StageManeger>().EnemyDamage(collision.gameObject,Attack,transform);
        }

        if(collision.tag != "Player")
        {
            Destroy(gameObject);
            maneger.BreakEffect(transform); 
        }

    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
