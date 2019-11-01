using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    public AudioClip BackMusic;
    bool ExitFlag = true;
    // Start is called before the first frame update
    void Start()
    {
        SoundManeger.instance.Music(BackMusic,1.0f);
        GameObject.Find("LoadManeger").GetComponent<SceneLoad>().DeathCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && ExitFlag)
        {
            StartCoroutine(TitleBack());
            SoundManeger.instance.Stop();
            ExitFlag = false;
        }
    }

    IEnumerator  TitleBack()
    {
        yield return new WaitForSeconds(0.3f);
        SceneLoad.instance.LoadScene("MainTitle");
    }
}
