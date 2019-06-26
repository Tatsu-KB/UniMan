using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [SerializeField] Animator animator;
    Rigidbody2D rb;
    BoxCollider2D col;
    public int MoveSpeed;
    float Horizontal,Vertical;
    private static int gravity = -1;
    public bool OnGround;
    float preScale, preScale_re;
   [SerializeField] GameObject Deth_Effect;
   public bool Active,NowAttack;
    AnimatorStateInfo nowAnim;
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
    }

    private void Update()
    {
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

            if (Input.GetButtonDown("Fire1"))
            {
                Attack1();
            }
            if (Input.GetButtonDown("Fire2"))
            {
                Attack2();
            }
            if (Input.GetButtonDown("Fire3"))
            {
                Attack3();
            }

            nowAnim = animator.GetCurrentAnimatorStateInfo(0);

            if ((nowAnim.IsName("Attack_1") || nowAnim.IsName("Attack_2") || nowAnim.IsName("Attack_3")) && nowAnim.normalizedTime >= 1.0f)
            {
                if (NowAttack)
                {
                    animator.SetTrigger("Stand");
                    NowAttack = false;
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
        if (NowAttack == false)
        {
            NowAttack = true;
            animator.SetTrigger("Attack1");
        }
    }
    void Attack2()
    {
        if (NowAttack == false)
        {
            NowAttack = true;
            animator.SetTrigger("Attack2");
        }
    }
    void Attack3()
    {
        if (NowAttack == false)
        {
            NowAttack = true;
            animator.SetTrigger("Attack3");
        }
    }

    public void IsGround()
    {
        if(rb.velocity.y <= 0) OnGround = true;
    }
    public void NotGround()
    {
        OnGround = false;

    }
    public void Damage()
    {
       if(Active) animator.SetTrigger("Damage");
        PlayerDown();
    }


    public void PlayerDown()
    {
        if(Active)animator.SetBool("Dead", true);
        Active = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        Invoke("PlayerFalse",1.0f);
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
        gameObject.SetActive(false);
    }

    public void StageClear()
    {
        Active = false;
        rb.velocity = new Vector2(0, transform.position.y);
    }
}
