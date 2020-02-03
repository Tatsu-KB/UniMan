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
    public bool ActiveFlag,OnceTime, SecondTime,AttackFlag = false;
    Animator anim;
    float Speed;
    float preScale, preScale_re;
    Vector3 scale;
    public GameObject bullet ,bomb;
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
            gameObject.layer = 8;
            rb.velocity = new Vector2(0, 0);
            anim.SetTrigger("Death");
            ActiveFlag = false;
        }
        Vector2 targetPos = player.transform.position;

        float x = targetPos.x;
        float y = targetPos.y;
        transform.localScale = scale;
        
        if(ActiveFlag && !player.activeSelf)
        {
            anim.SetTrigger("Winner");
            ActiveFlag = false;
        }
    }
    void FixedUpdate()
    {
        PosX = transform.position.x;
        PosY = transform.position.y;
        PosX = Mathf.Clamp(PosX, 313, 326);
        if (ActiveFlag && player.activeSelf)
        {
            if (Life > beforlife / 1.5 && !SecondTime)
            {
                JumpTimer -= Time.deltaTime;
                if (JumpTimer < 0)
                {
                    anim.SetTrigger("Jump");
                    rb.velocity = new Vector2(rb.velocity.x, 16.0f);
                    JumpTimer = Timer;
                }
            }
            if(Life <= beforlife / 1.5)
            {
                if(!SecondTime)
                {
                    anim.SetTrigger("second");
                    gameObject.layer = 8;
                    Speed = 0;
                    speed = 0;
                    SecondTime = true;
                }
                speed = 10;
                Vector2 targetPos = player.transform.position;
                float x = targetPos.x;
                float y = targetPos.y;
                Vector2 direction = new Vector2(x - transform.position.x, y - transform.position.y - 0.1f).normalized;
                Debug.Log(direction.x);
                if ((direction.x > 0.6f || direction.x < -0.6f) && AttackFlag)
                {
                    JumpTimer -= Time.deltaTime;
                }
                if(JumpTimer < 0)
                {
                    Speed = 0;
                    speed = 0;
                    //プレイヤーとの距離算出
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

                    JumpTimer = 3;
                }
            }
            if(transform.position.x < 306)
            {
                Speed = speed;
                scale.x = preScale_re;
            }
            if (transform.position.x > 320)
            {
                Speed = -speed;
                scale.x = preScale;
            }
            rb.velocity = new Vector2(Speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, -10);
        }
        anim.SetFloat("Speed",Mathf.Abs(Speed));
 //       Debug.Log(beforlife);
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
        this.gameObject.layer = 18;
    }
    void AttackFire()
    {
        Instantiate (bullet, new Vector3(transform.position.x + transform.localScale.x / 5, transform.position.y + 0.1f), bullet.transform.rotation).GetComponent<TokoBullet>().Inst(1 * ((int)transform.localScale.x / Mathf.Abs((int)transform.localScale.x)), 0);
        Instantiate(bullet, new Vector3(transform.position.x + transform.localScale.x / 5, transform.position.y + 0.4f), bullet.transform.rotation).GetComponent<TokoBullet>().Inst(2f * ((int)transform.localScale.x / Mathf.Abs((int)transform.localScale.x)), 0.5f);
        Instantiate(bullet, new Vector3(transform.position.x + transform.localScale.x / 5, transform.position.y - 0.2f), bullet.transform.rotation).GetComponent<TokoBullet>().Inst(1.5f * ((int)transform.localScale.x / Mathf.Abs((int)transform.localScale.x)), 1.25f);
        maneger.EnemyAttack();

    }
    void AttackEnd()
    {
        if(transform.localScale.x < 0)
        {
            Speed = -10;
            speed = 10;
            anim.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, 14.0f);
        }
        else
        {
            Speed = 10;
            speed = 10;
            anim.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, 14.0f);
        }
    }

    void Bomb()
    {
        Instantiate(bomb, new Vector3(transform.position.x , transform.position.y - 0.1f), bomb.transform.rotation);
    }

    public void Damage(int ATK)
    {
        Life -= ATK;
    }

    void DamageReturn()
    {
        gameObject.layer = 18;
        AttackFlag = true;
    }

    void KockDown()
    {
        if(Life<= 0)
        {
            maneger.GetComponent<StageManeger>().StageGoal(transform);
            maneger.GoalPerformance();
        }
    }
}
