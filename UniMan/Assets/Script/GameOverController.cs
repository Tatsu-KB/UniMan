using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    string SceneNeme;
    public Button Continue, Return;
    public int ButtonNum = 0;
    Selectable select;
    public bool AxisReset = false;
    // Start is called before the first frame update
    void Start()
    {
        select = GetComponent<Selectable>();
        SceneNeme = GameObject.Find("LoadManeger").GetComponent<SceneLoad>().BackName;
        StartCoroutine(ButtonSelect());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") != 0.0f && !AxisReset)
        {
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
        if (Input.anyKeyDown && Input.GetAxis("Vertical") == 0)
        {
            StartCoroutine(Select());
        }

        ButtonNum = Mathf.Clamp(ButtonNum, -1, 0);
    }

    IEnumerator ButtonSelect()
    {
        yield return new WaitForSeconds(0.3f);
        switch(ButtonNum)
        {
            case 0:
                Continue.Select();
                break;
            case -1:
                Return.Select();
                break;
        }
    }

    IEnumerator Select()
    {
        switch (ButtonNum)
        {
            case 0:
                SceneLoad.instance.LoadScene(SceneNeme);
                yield return new WaitForSeconds(0.5f);
                break;
            case -1:
                SceneLoad.instance.LoadScene("MainTitle");
                yield return new WaitForSeconds(0.5f);
                break;
        }
    }
}
