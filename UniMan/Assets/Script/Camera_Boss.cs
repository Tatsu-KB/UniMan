﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Boss : MonoBehaviour
{
    public CameraMove CameraMove;
    public AudioClip AudioClip;
    public GameObject Life;
    // Start is called before the first frame update
    void Start()
    {
        Life.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Life.SetActive(true);
            CameraMove.MinX = 313;
            CameraMove.MaxX = 313;
            CameraMove.MinY = 0;
            CameraMove.MaxY = 0;
            CameraMove.Boss = true;
            GameObject.Find("StageManeger").GetComponent<StageManeger>().clip = AudioClip;
            GameObject.Find("StageManeger").GetComponent<StageManeger>().Music();
            Debug.Log("エ”ェ”イ”!!");
        }
    }
    // X= 258
    //Y = 2
}
