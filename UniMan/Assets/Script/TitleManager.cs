using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class TitleManager : MonoBehaviour
{
    public Button button, st, ex;
    bool AxisReset = false,StartFlag = false;
    public int ButtonNum;
    string ButtonName;
    [SerializeField] string SceneName;
    public Animation Anime1, Anime2;
    Selectable select;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
        ButtonNum = 0;
    }

    void Start()
    {
        select = GetComponent<Selectable>();
        st.gameObject.SetActive(false);
        ex.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && !StartFlag)
        {
            foreach(KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
               if (Input.GetKeyDown(code))
               {
                    Title();
                    break;
               }
            }
        }
        if (StartFlag)
        {

            if (Input.GetAxisRaw("Vertical") != 0 && !AxisReset)
            {
                ButtonNum += (int)Input.GetAxisRaw("Vertical") * 2;
                AxisReset = true;
                StartCoroutine(ModeSelect());
            }
            if (Input.GetAxisRaw("Vertical") != 0) AxisReset = false;
            if (StartFlag && Input.anyKeyDown && Input.GetAxisRaw("Vertical") == 0) StartCoroutine(Select());
            ButtonNum = Mathf.Clamp(ButtonNum, -1, 0);

        }
    }
    public void Title()
    {
        button.gameObject.SetActive(false);
        st.gameObject.SetActive(true);
        ex.gameObject.SetActive(true);
        StartCoroutine(ModeSelect());
    }
    
    private IEnumerator ModeSelect()
    {
        yield return new WaitForSeconds(0.8f);
        if(!StartFlag)StartFlag = true;
        switch (ButtonNum)
        {
            case 0:
                st.Select();
                break;
            case -1:
                ex.Select();
                break;
        }
    }

    private IEnumerator Select()
    {

        switch (ButtonNum)
        {
            case 0:
                GameStart();
                yield return new WaitForSeconds(0.5f);
                break;
            case -1:
                GameExit();
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
}
