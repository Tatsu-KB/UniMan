﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bee : MonoBehaviour
{
    int Life;
    GameObject player;
    bool EnemyFlag;
    public float Speed;
    Rigidbody2D rb;
    float preScale, preScale_re;
    Vector3 scale;
    StageManeger maneger;
    int Attack;
    // Start is called before the first frame update
    void Start()
    {
        Life = 1;
        EnemyFlag = false;
        rb = GetComponent<Rigidbody2D>();
        preScale = transform.localScale.x;
        preScale_re = transform.localScale.x * -1;
        scale = transform.localScale;
        maneger = GameObject.FindGameObjectWithTag("StageManeger").GetComponent<StageManeger>();
        Attack = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(!player) player = GameObject.FindGameObjectWithTag("Player");


        if (EnemyFlag)
        {
            EnemyMove();
        }
    }

    void OnWillRenderObject()
    {
        //画面内に出るまでは動かさない
        if (Camera.current.name == "Main Camera")
                EnemyFlag = true;
    }

    void EnemyMove()
    {
        if (player.activeSelf)
        {
            Vector2 targetPos = player.transform.position;

            float x = targetPos.x;
            float y = targetPos.y;

            //プレイヤーとの距離算出
            Vector2 direction = new Vector2(x - transform.position.x, y - transform.position.y -0.1f).normalized;
            //プレイヤーに向け前進
            rb.velocity = direction * Speed;
            //Debug.Log(x - transform.position.x);

            //反転処理
            if ((x - transform.position.x) < 0 && player.activeSelf)
            {

                scale.x = preScale;
            }

            if ((x - transform.position.x) > 0 && player.activeSelf)
            {
                scale.x = preScale_re;
            }

            transform.localScale = scale;
        }

        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            maneger.PlayerDamege(Attack);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            maneger.PlayerDamege(Attack);
        }
    }



    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Damage(int EnemyATK)
    {
        Life -= EnemyATK;
        if (Life <= 0)
        {
            maneger.EnemyDown(transform);
            Destroy(gameObject);
        }
    }

}
