using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class TitleManager : MonoBehaviour
{
    public Button button, st, ex;

    private int ButtonNum;
    string ButtonName;
    [SerializeField] string SceneName;
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;   
    }

    void Start()
    {

        st.gameObject.SetActive(false);
        ex.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            foreach(KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
               if (Input.GetKeyDown(code))
                    {
                    Debug.Log(code);
                    break;
                }
            }
        }
    }
    public void Title()
    {
        button.gameObject.SetActive(false);
        st.gameObject.SetActive(true);
        ex.gameObject.SetActive(true);
    }

    public void GameStart()
    {

    }

    public void GameExit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
