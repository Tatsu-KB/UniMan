﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TitleManager : MonoBehaviour
{
    public TextMeshProUGUI button, st, ma, ex, op;
    bool AxisReset = false,StartFlag = false,AxisFlag = false;
    int ButtonNum;    //ボタン選択の番号、最大値、最小値
    static int Max;
    string ButtonName;
    [SerializeField] string SceneName;
    public Animation Anime1, Anime2;
    public AudioClip StartSE,BackMusic,SelectSE,CursolSE;
    bool MusicFlag = false;
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
        Max = GameObject.FindGameObjectsWithTag("Menu").Length - 1;
        ButtonNum = Max;
    }

    void Start()
    {
        st.gameObject.SetActive(false);
        ma.gameObject.SetActive(false);
        ex.gameObject.SetActive(false);
        op.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!MusicFlag) Music();
        if (Input.anyKeyDown && !StartFlag)
        {
            foreach(KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
               if (Input.GetKeyDown(code) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
               {
                    Title();
                    SoundManeger.instance.SoundPlayer(StartSE,1);
                    break;
               }
            }
        }
        if (StartFlag && AxisFlag)
        {

            if (Input.GetAxis("Vertical") != 0.0f && !AxisReset)
            {

                if (Input.GetAxis("Vertical") <= 0.0f && ButtonNum > 0)
                {
                    ButtonNum--;
                    SoundManeger.instance.SoundPlayer(CursolSE,1);
                }
                if (Input.GetAxis("Vertical") >= 0.0f && ButtonNum < Max)
                {
                    ButtonNum++;
                    SoundManeger.instance.SoundPlayer(CursolSE,1);
                }
                AxisReset = true;
                StartCoroutine(ModeSelect());
            }
            if (Input.GetAxis("Vertical") == 0.0f && AxisReset)
            {
                AxisReset = false;
            }

            if (StartFlag && Input.anyKeyDown && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && AxisFlag)
            {
                StartCoroutine(Select());
                AxisFlag = false;
            }
            ButtonNum = Mathf.Clamp(ButtonNum, 0, Max);
            

        }
    }
    public void Title()
    {
        button.gameObject.SetActive(false);
        st.gameObject.SetActive(true);
        ma.gameObject.SetActive(true);
        ex.gameObject.SetActive(true);
        op.gameObject.SetActive(true);
        StartCoroutine(ModeSelect());
        AxisFlag = true;
    }
    
    private IEnumerator ModeSelect()
    {

        if (!StartFlag)
        {
            yield return new WaitForSeconds(1.0f);
            StartFlag = true;
        }
        else yield return new WaitForSeconds(0.05f);
        switch (ButtonNum)
        {
            case 3:
                st.color = new Color(0, 1, 1, 1);
                ma.color = new Color(1, 1, 1, 1);
                ex.color = new Color(1, 1, 1, 1);
                op.color = new Color(1, 1, 1, 1);
                break;
            case 2:
                st.color = new Color(1, 1, 1, 1);
                ma.color = new Color(0, 1, 1, 1);
                ex.color = new Color(1, 1, 1, 1);
                op.color = new Color(1, 1, 1, 1);
                break;
            case 1:
                st.color = new Color(1, 1, 1, 1);
                ma.color = new Color(1, 1, 1, 1);
                ex.color = new Color(1, 1, 1, 1);
                op.color = new Color(0, 1, 1, 1);
                break;
            case 0:
                st.color = new Color(1, 1, 1, 1);
                ma.color = new Color(1, 1, 1, 1);
                ex.color = new Color(0, 1, 1, 1);
                op.color = new Color(1, 1, 1, 1);
                break;
        }
    }

    private IEnumerator Select()
    {
        switch (ButtonNum)
        {
            case 3:
                GameLoad("StageSelect");
                SE(SelectSE);
                SoundManeger.instance.Stop();
                yield return new WaitForSeconds(0.5f);
                break;
            case 2:
                GameLoad("Manual");
                SE(SelectSE);
                SoundManeger.instance.Stop();
                yield return new WaitForSeconds(0.5f);
                break;
            case 1:
                GameLoad("Option");
                SE(SelectSE);
                SoundManeger.instance.Stop();
                yield return new WaitForSeconds(0.5f);
                break;
            case 0:
                GameExit();
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

    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
#if UNITY_STANDALONE
        Application.Quit();
#endif
    }

    void Music()
    {
        SoundManeger.instance.Music(BackMusic,1.0f);
        MusicFlag = true;
        return;
    }
    void SE(AudioClip clip)
    {
        SoundManeger.instance.SoundPlayer(clip,1);
    }
}
