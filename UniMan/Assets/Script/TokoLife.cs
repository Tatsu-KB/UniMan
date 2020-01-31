using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokoLife : MonoBehaviour
{
    public GameObject Toko;
    public Boss_Toko BossLife;
    Slider slider;
    public Image image;
    public float LifeNow, LifeBefor;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        image = GameObject.Find("Canvas/TokoLife/Fill Area/Fill").GetComponent<Image>();
        slider.maxValue = BossLife.beforlife;
    }

    // Update is called once per frame
    void Update()
    {
        LifeBefor = BossLife.Life;
        LifeNow = Mathf.MoveTowards(LifeNow, LifeBefor, Time.deltaTime * 32f);
        slider.value = LifeNow;
        image.color = Color.magenta;
    }
}
