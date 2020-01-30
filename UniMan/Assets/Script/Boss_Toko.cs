using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Toko : MonoBehaviour
{
    GameObject player;
    [Header("移動速度")] public float speed;
    [Header("体力")] public float Life;
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

    }
    private void Update()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if(Life <= 0)
        {
            anim.SetBool("Death", true);
        }
        Vector2 targetPos = player.transform.position;

        float x = targetPos.x;
        float y = targetPos.y;
        transform.localScale = scale;
    }
    void FixedUpdate()
    {
        PosX = transform.position.x;
        PosY = transform.position.y;
        PosX = Mathf.Clamp(PosX, 313, 326);
        if (ActiveFlag)
        {
            JumpTimer -= Time.deltaTime;
            if (JumpTimer < 0)
            {
                anim.SetTrigger("Jump");
                rb.velocity = new Vector2(rb.velocity.x, 16.0f);
                JumpTimer = Timer;
            }
            if(transform.position.x < 307)
            {
                Speed = speed;
                scale.x = preScale_re;
            }
            if (transform.position.x > 319)
            {
                Speed = -speed;
                scale.x = preScale;
            }
            rb.velocity = new Vector2(Speed, rb.velocity.y);
        }
        Debug.Log(rb.velocity.y);
        anim.SetFloat("Speed",Mathf.Abs(Speed));
    }
    void OnWillRenderObject()
    {
        //画面内に出るまでは動かさない
        if (Camera.current.name == "Main Camera" && !OnceTime)
        {
            anim.SetTrigger("Start");
            OnceTime = true;
        }
    }
    public void ActionStart()
    {
        ActiveFlag = true;
        anim.SetTrigger("Action");
    }
}
