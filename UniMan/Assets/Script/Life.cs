﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    GameObject Player;
    Player_Move PlayerLife;
    Slider slider;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Player)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            PlayerLife = Player.GetComponent<Player_Move>();
            slider.maxValue = PlayerLife.Life;

        }

        slider.value = PlayerLife.Life;
        if (slider.value <= slider.maxValue / 2)
        {
            image.color = Color.yellow;
            if (slider.value < slider.maxValue / 4)
            {
                image.color = Color.red;
            }
        }
        else
            image.color = Color.cyan;
    }
}
