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
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        OnGround = false;
        preScale = transform.localScale.x;
        preScale_re = transform.localScale.x * -1;

    }

    private void Update()
    {
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
    }

    // Update is called once per frame
    void  FixedUpdate()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Mathf.Clamp(rb.velocity.y,-1,1);
        Move(Horizontal);

        Attack();

    }
    void Move(float X)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        Vector3 scale = transform.localScale;

        
        rb.velocity = new Vector2(Horizontal * MoveSpeed,rb.velocity.y);
        
        animator.SetFloat("Move",Mathf.Abs( Horizontal));

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

    void Attack()
    {
        float nowAnim = animator.GetNextAnimatorStateInfo(0).normalizedTime;
        Debug.Log(nowAnim);
        if(Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack1");
            
        }

        if (nowAnim > 1) animator.SetTrigger("Stand");
    }

    public void IsGround()
    {
        if(rb.velocity.y <= 0) OnGround = true;
    }
    public void NotGround()
    {
        OnGround = false;
    }
}
