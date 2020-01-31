using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Toko : MonoBehaviour
{
    GameObject player;
    [Header("移動速度")] public float speed;
    [Header("体力")] public float Life;
    public float beforlife;
    float PosX;
    float PosY;
    Rigidbody2D rb;
    BoxCollider2D col;
    public float JumpTimer;
    float Timer;
    StageManeger maneger;
    public bool ActiveFlag,OnceTime;
    Animator anim;
    float Speed;
    float preScale, preScale_re;
    Vector3 scale;
    public GameObject Bullet;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        maneger = GameObject.FindGameObjectWithTag("StageManeger").GetComponent<StageManeger>();
        ActiveFlag = false;
        anim = GetComponent<Animator>();
        Timer = JumpTimer;
        Speed = -speed;
        preScale = transform.localScale.x;
        preScale_re = transform.localScale.x * -1;
        scale = transform.localScale;
        beforlife = Life;
        Life = 1;
    }
    private void Update()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if(Life <= 0 && ActiveFlag)
        {
            Debug.Log("やったー！ブッパー！");
            rb.velocity = new Vector2(0, 0);
            anim.SetTrigger("Death");
            ActiveFlag = false;
        }
        Vector2 targetPos = player.transform.position;

        float x = targetPos.x;
        float y = targetPos.y;
        transform.localScale = scale;
        if (Input.GetKeyDown(KeyCode.W))
        {
            Life--;
        }
    }
    void FixedUpdate()
    {
        PosX = transform.position.x;
        PosY = transform.position.y;
        PosX = Mathf.Clamp(PosX, 313, 326);
        if (ActiveFlag)
        {
            if (Life >= beforlife / 2)
            {
                JumpTimer -= Time.deltaTime;
                if (JumpTimer < 0)
                {
                    anim.SetTrigger("Jump");
                    rb.velocity = new Vector2(rb.velocity.x, 16.0f);
                    JumpTimer = Timer;
                }
            }
            if(Life < beforlife / 2)
            {
                speed = 7;
                JumpTimer -= Time.deltaTime;
                if(JumpTimer < 0)
                {
                    Vector2 targetPos = player.transform.position;

                    float x = targetPos.x;
                    float y = targetPos.y;
                    //プレイヤーとの距離算出
                    Vector2 direction = new Vector2(x - transform.position.x, y - transform.position.y - 0.1f).normalized;
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
                    anim.SetTrigger("Attack1");
                    Speed = 0;
                    JumpTimer = 3;
                }
            }
            if(transform.position.x < 306.5)
            {
                Speed = speed;
                scale.x = preScale_re;
            }
            if (transform.position.x > 319.5)
            {
                Speed = -speed;
                scale.x = preScale;
            }
            rb.velocity = new Vector2(Speed, rb.velocity.y);
        }
        anim.SetFloat("Speed",Mathf.Abs(Speed));
        Debug.Log(beforlife);
    }
    void OnWillRenderObject()
    {
        //画面内に出るまでは動かさない
        if (Camera.current.name == "Main Camera" && !OnceTime)
        {
            anim.SetTrigger("Start");
            Life = beforlife;
            OnceTime = true;
        }
    }
    public void ActionStart()
    {
        ActiveFlag = true;
        anim.SetTrigger("Action");
    }
    void AttackFire()
    {
        Instantiate(Bullet, transform.position, transform.rotation).transform.parent = null;
    }
    void AttackEnd()
    {
        if(transform.localScale.x < 0)
        {
            Speed = -7;
            anim.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, 14.0f);
        }
        else
        {
            Speed = 7;
            anim.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, 14.0f);
        }
    }
}
