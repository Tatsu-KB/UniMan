using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyControll : MonoBehaviour
{
    Animator animator;
    int value;
    float action;
    public enum EnemyState
    {
        Idle,
        Move,
        Attack1,
        Attack2,
        BodyAttack,
        Damege,
        Dead,
        PlayerDead,
    };
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo nowAnim = animator.GetCurrentAnimatorStateInfo(0);
        action += Time.deltaTime;
        /*
        if (nowAnim.normalizedTime >= 1.0f)
        {
            State();
            
        }
        */
        if(action >= 1.0f)
        {
            State();
            action = 0;
        }
        switch(value)
        {
            case 0:
               // Debug.Log("0");
                break;
            case 1:
               // Debug.Log("1");
                break;
            case 2:
                //Debug.Log("2");
                break;
        }
    }

    void State()
    {
        value = Random.Range(0, 3);
        Debug.Log(value);
    }
}
