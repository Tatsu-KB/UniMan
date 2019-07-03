using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject player;
    Vector2 direction;
    public float SpeedX, SpeedY;
    public int Attack;
    StageManeger maneger;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        maneger = GameObject.FindGameObjectWithTag("StageManeger").GetComponent<StageManeger>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

}
