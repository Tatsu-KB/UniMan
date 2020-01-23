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
    public float JumpTimer = 3.0f;
    StageManeger maneger;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        maneger = GameObject.FindGameObjectWithTag("StageManeger").GetComponent<StageManeger>();

    }
    private void Update()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player");
    }
    void FixedUpdate()
    {
        PosX = transform.position.x;
        PosY = transform.position.y;

        PosX = Mathf.Clamp(PosX, 251.2f, 264.8f);
        JumpTimer -= Time.deltaTime;
        if(JumpTimer< 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 18.0f);
            JumpTimer = 3.0f;
        }
    }


}
