using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bee : MonoBehaviour
{
    GameObject player;
    public bool EnemyFlag;
    public Renderer Renderer;
    float Min, Max;
    [SerializeField]float Speed;
    [SerializeField] Rigidbody2D rb;
    float preScale, preScale_re;
    public Vector3 scale;


    // Start is called before the first frame update
    void Start()
    {
        Min = transform.position.y;
        Max = transform.position.y + 3;
        EnemyFlag = false;
        Renderer = GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        preScale = transform.localScale.x;
        preScale_re = transform.localScale.x * -1;
        scale = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyFlag)
        {
            //transform.position = new Vector3(transform.position.x ,   transform.position.y , transform.position.z);
            EnemyMove();
        }
    }

    void OnWillRenderObject()
    {
        if (Camera.current.name == "SceneCamera")
        {

        }

        if (Camera.current.name == "Main Camera")
                EnemyFlag = true;
    }

    void EnemyMove()
    {
        Vector2 targetPos = player.transform.position;

        float x = targetPos.x;
        float y = targetPos.y;

        Vector2 direction = new Vector2(x - transform.position.x, y - transform.position.y).normalized;

        rb.velocity = direction * Speed;
        Debug.Log(x - transform.position.x);

        
        if((x - transform.position.x) <= 0)
        {

                scale.x = preScale;
        }
        
        if((x - transform.position.x) >= 0)
        {
                scale.x = preScale_re;
        }

        transform.localScale = scale;
    }
}
