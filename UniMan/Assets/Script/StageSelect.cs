using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class StageSelect : MonoBehaviour
{
    public Image Stage1, Stage2;
    bool AxisReset = false, AxisFlag = false;
    public int ButtonNum , Max;
    [SerializeField] string SceneName;
    public AudioClip BackMusic, CursolSE, SelectSE;
    bool MusicFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        Max = GameObject.FindGameObjectsWithTag("Menu").Length - 1;
        ButtonNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!MusicFlag) Music();
        StartCoroutine(ButtonSelect());
        if (Input.GetAxis("Horizontal") != 0.0f && !AxisReset)
        {

            if (Input.GetAxis("Horizontal") <= 0.0f && ButtonNum > 0)
            {
                ButtonNum--;
                SoundManeger.instance.Sound(CursolSE);
            }
            if (Input.GetAxis("Horizontal") >= 0.0f && ButtonNum < Max)
            {
                ButtonNum++;
                SoundManeger.instance.Sound(CursolSE);
            }
            AxisReset = true;
        }
        if (Input.GetAxis("Horizontal") == 0.0f && AxisReset)
        {
            AxisReset = false;
        }

        ButtonNum = Mathf.Clamp(ButtonNum, 0, Max);

        if (Input.anyKeyDown && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
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
            case 1:
                Stage1.color = new Color(1, 1, 1, 1);
                Stage2.color = new Color(1, 1, 1, 0.5f);
                break;
            case 0:
                Stage2.color = new Color(1, 1, 1, 1);
                Stage1.color = new Color(1, 1, 1, 0.5f);
                break;
        }
    }
    private IEnumerator Select()
    {
        switch (ButtonNum)
        {
            case 0:
                GameLoad("Stage1");
                SE(SelectSE);
                SoundManeger.instance.Stop();
                yield return new WaitForSeconds(0.5f);
                break;
            case 1:
                GameLoad("Stage2");
                SE(SelectSE);
                SoundManeger.instance.Stop();
                yield return new WaitForSeconds(0.5f);
                break;
        }
    }
    public void GameLoad(string Name)
    {
        SceneName = Name;
        SceneLoad.instance.LoadScene(SceneName);
    }

    void Music()
    {
        SoundManeger.instance.Music(BackMusic, 1.0f);
        MusicFlag = true;
        return;
    }
    void SE(AudioClip clip)
    {
        SoundManeger.instance.Sound(clip);
    }
}
