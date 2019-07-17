﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class TitleManager : MonoBehaviour
{
    public TextMeshProUGUI button, st, ex;
    public bool AxisReset = false,StartFlag = false,AxisFlag = false;
    public int ButtonNum;
    string ButtonName;
    [SerializeField] string SceneName;
    public Animation Anime1, Anime2;
    public AudioClip BackMusic,SelectSE,CursolSE;
    bool Flag = false;
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
        ButtonNum = 0;
    }

    void Start()
    {
        st.gameObject.SetActive(false);
        ex.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (!Flag) Music();
        if (Input.anyKeyDown && !StartFlag)
        {
            foreach(KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
               if (Input.GetKeyDown(code) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
               {
                    Title();
                    break;
               }
            }
        }
        if (StartFlag && AxisFlag)
        {

            if (Input.GetAxis("Vertical") != 0.0f && !AxisReset)
            {
                if(Input.GetAxis("Vertical") <= 0.0f)
                    ButtonNum--;
                    SoundManeger.instance.Sound(CursolSE);
                if (Input.GetAxis("Vertical") >= 0.0f)
                    ButtonNum++;
                    SoundManeger.instance.Sound(CursolSE);
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
            ButtonNum = Mathf.Clamp(ButtonNum, -1, 0);
            

        }
    }
    public void Title()
    {
        button.gameObject.SetActive(false);
        st.gameObject.SetActive(true);
        ex.gameObject.SetActive(true);
        StartCoroutine(ModeSelect());
        AxisFlag = true;
    }
    
    private IEnumerator ModeSelect()
    {

        if (!StartFlag)
        {
            yield return new WaitForSeconds(0.8f);
            StartFlag = true;
        }
        else yield return new WaitForSeconds(0.05f);
        switch (ButtonNum)
        {
            case 0:
                st.color = new Color(0, 1, 1, 1);
                ex.color = new Color(1, 1, 1, 1);
                break;
            case -1:
                ex.color = new Color(0, 1, 1, 1);
                st.color = new Color(1, 1, 1, 1);
                break;
        }
    }

    private IEnumerator Select()
    {

        switch (ButtonNum)
        {
            case 0:
                GameStart();
                SE(SelectSE);
                SoundManeger.instance.Stop();
                yield return new WaitForSeconds(0.5f);
                break;
            case -1:
                GameExit();
                SE(SelectSE);
                SoundManeger.instance.Stop();
                yield return new WaitForSeconds(0.5f);
                break;
        }
    }


    public void GameStart()
    {
        SceneName = "Stage_Tutorial";
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
        SoundManeger.instance.Music(BackMusic,0.8f);
        Flag = true;
        return;
    }
    void SE(AudioClip clip)
    {
        SoundManeger.instance.Sound(clip);
    }
}
