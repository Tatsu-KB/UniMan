using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    StageManeger stageManeger;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        stageManeger = GameObject.Find("StageManeger").GetComponent<StageManeger>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            stageManeger.StageGoal();
            animator.SetTrigger("Get");
            Invoke("Lost", 0.5f);
        }
    }
    void Lost()
    {
        gameObject.SetActive(false);
    }
}
