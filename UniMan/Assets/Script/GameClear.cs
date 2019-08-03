using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            StartCoroutine(TitleBack());
        }
    }

    IEnumerator  TitleBack()
    {
        yield return new WaitForSeconds(0.3f);
        SceneLoad.instance.LoadScene("MainTitle");
    }
}
