﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    StageManeger stageManeger;
    Animator animator;
    bool Active;
    // Start is called before the first frame update
    void Start()
    {
        stageManeger = GameObject.Find("StageManeger").GetComponent<StageManeger>();
        animator = GetComponent<Animator>();
        Active = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && Active)
        {
            stageManeger.StageGoal();
            animator.SetTrigger("Get");
            Invoke("Lost", 0.4f);
            Active = false;
        }
    }
    void Lost()
    {
        gameObject.SetActive(false);
        stageManeger.GoalPerformance();
    }
}
