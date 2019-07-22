﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    BoxCollider2D col;
    CircleCollider2D circle;
    public int MoveSpeed, Speed;                                            //移動速度
    float Horizontal, Vertical, preScale, preScale_re;    //横と縦の移動値、反転用の値
    public bool OnGround, Active = false, NowAttack;                        //接地、行動可能か、攻撃中かどうか                                
    AnimatorStateInfo nowAnim;                                  //アニメーションの情報取得
    Renderer ren;
    public int Life;                                                         //体力値
    StageManeger maneger;
    [SerializeField] GameObject Bullet;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        circle = GetComponent<CircleCollider2D>();
        OnGround = false;
        preScale = transform.localScale.x;
        preScale_re = transform.localScale.x * -1;
        NowAttack = false;
        maneger = GameObject.FindGameObjectWithTag("StageManeger").GetComponent<StageManeger>();
        ren = GetComponent<Renderer>();
        Life += maneger.LifeUp;
    }

    private void Update()
    {

        if (Active)

        {

            Horizontal = Input.GetAxisRaw("Horizontal");
            Vertical = Mathf.Clamp(rb.velocity.y, -1, 1);


            //Attack();
            var jumpPower = 15.0f;
            if (OnGround && (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
            if (OnGround == false && rb.velocity.y > 0.0f && (Input.GetKey(KeyCode.Space) || Input.GetButton("Jump")))
                rb.gravityScale = 0.5f;
            else rb.gravityScale = 1.0f;
            if (Active)
            {
                if (Input.GetButtonDown("Fire1") && !NowAttack)
                {
                    NowAttack = true;
                    Attack1();
                }
                if (Input.GetButtonDown("Fire2") && !NowAttack)
                {
                    NowAttack = true;
                    Attack2();
                }
                if (Input.GetButtonDown("Fire3") && !NowAttack)
                {
                    NowAttack = true;
                    Attack3();
                }
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (Active) Move(Horizontal);

        else
        {
            Horizontal = 0;
            Vertical = 0;
        }
        animator.SetFloat("Move", Mathf.Abs(Horizontal));
    }
    void Move(float X)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));  //カメラの左下座標の取得
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //カメラの右上     〃
        Vector2 pos = transform.position;   //自分の座標の更新
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);       //移動制限(カメラの表示範囲の横から出ないように)
        pos.y = Mathf.Clamp(pos.y, min.y - 3, max.y); //移動制限(カメラの表示範囲の縦から出ないように)
        Vector3 scale = transform.localScale;

        rb.velocity = new Vector2(Horizontal * MoveSpeed, rb.velocity.y); //移動


        if (!OnGround)  //地面に触れているか否か
        {
            animator.SetFloat("Jump", Mathf.Clamp(Vertical, -1.0f, 1.0f));
        }
        else
        {
            animator.SetFloat("Jump", 0.0f);
        }

        if (Horizontal > 0)
        {

            scale.x = preScale;
        }
        else if (Horizontal < 0)
        {
            scale.x = preScale_re;
        }


        transform.localScale = scale;
        transform.position = pos;
    }

    void Attack1()
    {
        animator.SetTrigger("Attack1");
        maneger.Attack();
    }
    void Attack2()
    {
        animator.SetTrigger("Attack2");
        maneger.Attack();
    }
    void Attack3()
    {
        animator.SetTrigger("Attack3");
        maneger.Attack();
    }

    public void Damage(int EnemyATK)
    {
        gameObject.layer = 8;
        ren.material.color = new Color(1, 1, 1, 0.5f);

        rb.velocity = Vector2.zero;
        if (Active)
        {
            animator.SetTrigger("Damage");
            maneger.Damege(transform);
            rb.isKinematic = true;
            Life -= EnemyATK;
            Active = false;
        }

        if (Life <= 0) PlayerDown(); //ライフ0でダウン
        else Invoke("Alive", 0.3f);
    }


    public void PlayerDown()
    {
        if (Active) animator.SetTrigger("Damage");
        Life = 0;
        Active = false;
        rb.velocity = Vector2.zero;                     // やられた後不自然に滑らないように  
        rb.isKinematic = true;                            //  天井のトゲなどでダウンした場合にその場で爆発させるため
        Invoke("PlayerFalse", 0.2f);                   //やられモーションを見せるため一瞬待っています
    }

    void Alive()
    {
        animator.SetTrigger("Stand");
        Invoke("Find", 1.5f);                //無敵時間を解除するまでの時間
        Active = true;                        //操作不能を解除
        rb.isKinematic = false;          //ダメージを受けた際物理計算を止めるので元に戻す
    }
    void PlayerFalse()
    {
        maneger.DownEffect(transform);          //自分の居場所にエフェクトを出す
        gameObject.SetActive(false);               //ダウンと同時だとエフェクトが不自然だったため
    }
    public void StageClear()
    {
        Active = false;
        rb.velocity = new Vector2(0, transform.position.y);
    }

    public void Performance()
    {
        gameObject.layer = 8;
        int value = Random.Range(0, 2);
        if (value == 0)
        {
            animator.SetTrigger("Clear1");
        }
        if (value == 1)
        {
            animator.SetTrigger("Clear2");
        }
    }
    void AnimationEnd()
    {
        animator.SetTrigger("Stand");
        NowAttack = false;
    }

    void Find()
    {
        gameObject.layer = 1;
        ren.material.color = new Color(1, 1, 1, 1);

    }

    public void Attack_Front()
    {
        Instantiate
        (Bullet, new Vector3(transform.position.x + transform.localScale.x / 5, transform.position.y + 0.1f)
        , Bullet.transform.rotation)
        .GetComponent<PlayerBullet>().Inst(1 * ((int)transform.localScale.x / Mathf.Abs((int)transform.localScale.x)), 0);

    }
    public void Attack_Up()
    {
        Instantiate
        (Bullet, new Vector3(transform.position.x + transform.localScale.x / 5, transform.position.y + 0.4f)
        , Bullet.transform.rotation)
        .GetComponent<PlayerBullet>().Inst(1.5f * ((int)transform.localScale.x / Mathf.Abs((int)transform.localScale.x)), 1);
    }
    public void Attack_Down()
    {
        Instantiate
     (Bullet, new Vector3(transform.position.x + transform.localScale.x / 5, transform.position.y - 0.2f)
    , Bullet.transform.rotation)
     .GetComponent<PlayerBullet>().Inst(1.5f * ((int)transform.localScale.x / Mathf.Abs((int)transform.localScale.x)), -1);

    }
    void ActiveStart()
    {
        Active = true;
    }

    public void IsGround()
    {
        if (rb.velocity.y <= 0)
        {
            OnGround = true;
            Collider();
        }
    }
    public void NotGround()
    {
        OnGround = false;

    }
    //動く床に対応させるために子オブジェクトにする
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MoveGround")
        {
            transform.parent = collision.gameObject.transform;
        }
        
        if (!OnGround && collision.gameObject.tag == "OneWayGround" && rb.velocity.y == 0)
        {
            col.isTrigger = true;
            circle.isTrigger = true;
        }
        
    }
    //動く床から離れたら子オブジェクトを解除
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MoveGround")
        {
            transform.parent = null;
        }
        
        if(collision.gameObject.tag == "OneWayGround")
        {
            Collider();
        }
        
    }

    private void Collider()
    {
        col.isTrigger = false;
        circle.isTrigger = false;
    }
}