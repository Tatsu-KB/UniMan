﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    [SerializeField] StageManeger StageManeger;
    // Start is called before the first frame update
    void Start()
    {
        StageManeger = GameObject.Find("StageManeger").GetComponent<StageManeger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StageManeger.NeedleDamage();
        }
    }
}
