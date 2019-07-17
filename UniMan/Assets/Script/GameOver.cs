using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    string SceneNeme;
    public TextMeshProUGUI Continue, Return;
    public int ButtonNum = 0;
    public bool AxisReset = false;
    public AudioClip BGM,SelectSE,CursolSE;

    // Start is called before the first frame update
    void Start()
    {
        SceneNeme = GameObject.Find("LoadManeger").GetComponent<SceneLoad>().BackName;
        StartCoroutine(ButtonSelect());
        SoundManeger.instance.Music(BGM,1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") != 0.0f && !AxisReset)
        {
            SoundManeger.instance.Sound(CursolSE);
            if (Input.GetAxis("Vertical") <= 0.0f)
                ButtonNum--;
            if (Input.GetAxis("Vertical") >= 0.0f)
                ButtonNum++;
            AxisReset = true;
            StartCoroutine(ButtonSelect());
            Debug.Log(ButtonNum);
        }
        if (Input.GetAxis("Vertical") == 0.0f && AxisReset)
        {
            AxisReset = false;
        }
        if (Input.anyKeyDown && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            SoundManeger.instance.Sound(SelectSE);
            StartCoroutine(Select());
        }

        ButtonNum = Mathf.Clamp(ButtonNum, -1, 0);
    }

    IEnumerator ButtonSelect()
    {
        yield return new WaitForSeconds(0.05f);
        switch(ButtonNum)
        {
            case 0:
                Continue.color = new Color(0, 1, 1, 1);
                Return.color = new Color(1, 1, 1, 1);
                break;
            case -1:
                Return.color = new Color(0, 1, 1, 1);
                Continue.color = new Color(1, 1, 1, 1);
                break;
        }
    }

    IEnumerator Select()
    {
        switch (ButtonNum)
        {
            case 0:
                SceneLoad.instance.LoadScene(SceneNeme);
                SoundManeger.instance.Stop();
                yield return new WaitForSeconds(0.5f);
                break;
            case -1:
                SceneLoad.instance.LoadScene("MainTitle");
                SoundManeger.instance.Stop();
                yield return new WaitForSeconds(0.5f);
                break;
        }
    }
}
