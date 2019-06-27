using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    BoxCollider2D col;                                                      
    public int MoveSpeed;                                            //移動速度
    float Horizontal,Vertical, preScale, preScale_re;    //横と縦の移動値、反転用の値
    public bool OnGround,Active, NowAttack;                        //接地、行動可能か、攻撃中かどうか                                
    AnimatorStateInfo nowAnim;                                  //アニメーションの情報取得

    public int Life;                                                         //体力値
    StageManeger maneger;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        OnGround = false;
        preScale = transform.localScale.x;
        preScale_re = transform.localScale.x * -1;
        Active = true;
        NowAttack = false;
        maneger = GameObject.FindGameObjectWithTag("StageManeger").GetComponent<StageManeger>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Life = 0;
            Damage(5);
        }
        if (Active == true)

        {
            //Attack();
            var jumpPower = 15.0f;
            if (OnGround && (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")))
            {
                Debug.Log("Space");
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
            if (OnGround == false && rb.velocity.y > 0.0f && (Input.GetKey(KeyCode.Space) || Input.GetButton("Jump")))
            {
                rb.gravityScale = 0.5f;
            }
            else
            {
                rb.gravityScale = 1.0f;
            }
            //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            //Debug.Log(stateInfo.normalizedTime);

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

            nowAnim = animator.GetCurrentAnimatorStateInfo(0);
            //Debug.Log(nowAnim.length);
            if (nowAnim.normalizedTime < 1.0f) animator.Update(0);
            if ((nowAnim.IsName("Attack_1") || nowAnim.IsName("Attack_2") || nowAnim.IsName("Attack_3")) && nowAnim.normalizedTime >= 1.0f)
            {
                if (NowAttack)
                {
                    animator.SetTrigger("Stand");
                    Invoke("FlagReset", 0.4f);
                }
            }
           // Debug.Log(nowAnim.normalizedTime);
        }
    }
        // Update is called once per frame
        void FixedUpdate()
        {

        if (Active)
            {
                Horizontal = Input.GetAxisRaw("Horizontal");
                Vertical = Mathf.Clamp(rb.velocity.y, -1, 1);

                Move(Horizontal);
             }
            else
        {
            Horizontal = 0;
            Vertical = 0;
        }
        animator.SetFloat("Move", Mathf.Abs(Horizontal));
    }
    void Move(float X)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y - 3, max.y);

        Vector3 scale = transform.localScale;


        rb.velocity = new Vector2(Horizontal * MoveSpeed, rb.velocity.y);


        if (!OnGround)
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
    }
    void Attack2()
    {
            animator.SetTrigger("Attack2");
    }
    void Attack3()
    {
            animator.SetTrigger("Attack3");
    }

    public void IsGround()
    {
        if(rb.velocity.y <= 0) OnGround = true;
    }
    public void NotGround()
    {
        OnGround = false;

    }
    public void Damage( int EnemyATK)
    {
        rb.velocity = Vector2.zero;
        if (Active)
        {
            animator.SetTrigger("Damage");
            maneger.Damege(transform);
            Life -= EnemyATK;
        }

        if (Life == 0) PlayerDown(); //ライフ0でダウン
        else Invoke("Alive",0.5f);
    }

    
    public void PlayerDown()
    {
        if (Active) animator.SetTrigger("Damage");
        Active = false;
        rb.velocity = Vector2.zero;                     // やられた後不自然に滑らないように  
        rb.isKinematic = true;                            //  天井のトゲなどでダウンした場合にその場で爆発させるため
        Invoke("PlayerFalse",0.2f);                   //やられモーションを見せるため一瞬待っています
    }

    void Alive()
    {
        animator.SetTrigger("Stand");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "MoveGround")
        {
            transform.parent = col.gameObject.transform;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "MoveGround")
        {
            transform.parent = null;
        }
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
        int value = Random.Range(0, 2);
        if (value == 0)
        {
            animator.SetTrigger("Clear1");
        }
        if(value == 1)
        {
            animator.SetTrigger("Clear2");
        }
    }

    void FlagReset()
    {
        NowAttack = false;
    }
}
