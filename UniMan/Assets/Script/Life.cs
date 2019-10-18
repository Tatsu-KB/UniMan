using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    GameObject Player;
    public Player_Move PlayerLife;
    Slider slider;
    public Image image;
    float LifeNow;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        image = GameObject.Find("Canvas/Slider/Fill Area/Fill").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Player)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            PlayerLife = Player.GetComponent<Player_Move>();
            slider.maxValue = PlayerLife.Life;
            LifeNow = slider.maxValue;
        }
        if(LifeNow != PlayerLife.Life)
        {
            Mathf.Lerp(LifeNow,PlayerLife.Life,Time.deltaTime / 60.0f);
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
