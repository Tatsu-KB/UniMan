using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class PlayerSelect : MonoBehaviour
{
    public Image GunImage, SwordImage;

    public GameObject Gun, Sword;
    public Animator GunAnim, SwordAnim;
    bool AxisReset = false, AxisFlag = true;
    public int ButtonNum, Max;
    [SerializeField] string SceneName;
    public AudioClip BackMusic, CursolSE, SelectSE;
    bool MusicFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        Max = GameObject.FindGameObjectsWithTag("Player").Length - 1;
        ButtonNum = 0;
        GunAnim = Gun.GetComponent<Animator>();
        SwordAnim = Sword.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ButtonSelect());
        if (AxisFlag)
        {
            if (Input.GetAxis("Horizontal") != 0.0f && !AxisReset)
            {

                if (Input.GetAxis("Horizontal") <= 0.0f && ButtonNum > 0)
                {
                    ButtonNum--;
                    SoundManeger.instance.Sound(CursolSE,1, 1);
                }
                if (Input.GetAxis("Horizontal") >= 0.0f && ButtonNum < Max)
                {
                    ButtonNum++;
                    SoundManeger.instance.Sound(CursolSE,1, 1);
                }
                AxisReset = true;
            }
            if (Input.GetAxis("Horizontal") == 0.0f && AxisReset)
            {
                AxisReset = false;
            }
            ButtonNum = Mathf.Clamp(ButtonNum, 0, Max);

        }


        if (Input.anyKeyDown && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && AxisFlag)
        {
            StartCoroutine(Select());
            AxisFlag = false;
        }
    }
    IEnumerator ButtonSelect()
    {
        yield return new WaitForSeconds(0.05f);
        switch (ButtonNum)
        {
            case 0:

                GunAnim.SetBool("Select", true);
                SwordAnim.SetBool("Select", false);
                SwordImage.color = new Color(1, 1, 1, 1);
                GunImage.color = new Color(1, 1, 1, 0.5f);
                break;
            case 1:
                GunAnim.SetBool("Select", false);
                SwordAnim.SetBool("Select", true);
                GunImage.color = new Color(1, 1, 1, 1);
                SwordImage.color = new Color(1, 1, 1, 0.5f);
                break;
        }
    }
    private IEnumerator Select()
    {
        switch (ButtonNum)
        {
            case 0:
                GunAnim.SetTrigger("Decision");
                SoundManeger.instance.Stop();
                yield return new WaitForSeconds(0.5f);
                break;
            case 1:
                SwordAnim.SetTrigger("Decision");
                SoundManeger.instance.Stop();
                yield return new WaitForSeconds(0.5f);
                break;
        }
    }
    public void GameLoad()
    {
        SceneLoad.instance.LoadScene("StageSelect");
    }

    void Music()
    {
        SoundManeger.instance.Music(BackMusic, 1.0f);
        MusicFlag = true;
        return;
    }
    void SE(AudioClip clip)
    {
        SoundManeger.instance.Sound(clip,1, 1);
    }
}
