using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Piranha : MonoBehaviour
{
    GameObject player;
    public GameObject Bullet;
    bool EnemyFlag, HitFlag;
//    public float Speed;
    Rigidbody2D rb;
    float preScale, preScale_re;
    Vector3 scale;
    StageManeger maneger;
    Animator animator;
    int Attack;
    public bool StateFlag,Active;
    // Start is called before the first frame update
    void Start()
    {
        EnemyFlag = false;
        rb = GetComponent<Rigidbody2D>();
        preScale = transform.localScale.x;
        preScale_re = transform.localScale.x * -1;
        scale = transform.localScale;
        maneger = GameObject.FindGameObjectWithTag("StageManeger").GetComponent<StageManeger>();
        Attack = 2;
        animator = GetComponent<Animator>();
        StateFlag = false;
        Active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player");


        if (EnemyFlag)
        {
            EnemyAction();
            if(StateFlag == false)
            {
                StartCoroutine(AttackStart(5.5f));
                StateFlag = true;
            }
        }

    }
    void OnWillRenderObject()
    {
        //画面内に出るまでは動かさない
        if (Camera.current.name == "Main Camera")
            EnemyFlag = true;
    }
    void EnemyAction()
    {
        if (player.activeSelf)
        {
            Vector2 targetPos = player.transform.position;

            float x = targetPos.x;
            float y = targetPos.y;

            //プレイヤーとの距離算出
            Vector2 direction = new Vector2(x - transform.position.x, y - transform.position.y).normalized;
            //プレイヤーに向け前進
            //rb.velocity = direction * Speed;
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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            maneger.PlayerDamege(Attack);
            HitFlag = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        HitFlag = true;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            maneger.PlayerDamege(Attack);
            HitFlag = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        HitFlag = true;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private IEnumerator AttackStart(float delayCount)
    {
        while (true)
        {
            yield return new WaitForSeconds(delayCount);
            Action();
        }
    }


    void Action()
    {

        Vector2 Position = new Vector2(transform.position.x + -1 * transform.localScale.x / 2 / Mathf.Abs(transform.localScale.x),transform.position.y);
        if (Active)
        {
            animator.SetTrigger("Attack");
            Instantiate(Bullet,Position,transform.rotation).transform.parent = null;
        }
    }

    public void ActiveFalse()
    {
        Debug.Log("test");
        Active = false;  
    }
}
