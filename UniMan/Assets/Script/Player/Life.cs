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
    public float LifeNow,LifeBefor;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        image = GameObject.Find("Canvas/PlayerLife/Fill Area/Fill").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Player)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            PlayerLife = Player.GetComponent<Player_Move>();
            slider.maxValue = PlayerLife.Life;
            LifeBefor = PlayerLife.Life;
        }
        LifeBefor = PlayerLife.Life;
        LifeNow =  Mathf.MoveTowards(LifeNow, LifeBefor, Time.deltaTime * 36f);

        slider.value = LifeNow;
        if (slider.value <= slider.maxValue / 2)
        {
            image.color = Color.yellow;
            if (slider.value < slider.maxValue / 4)
            {
                image.color = Color.red;
            }
        }
        else
        image.color = Color.green;
    }
}
