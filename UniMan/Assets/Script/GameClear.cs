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
        if(Input.anyKeyDown && Input.GetAxis("Vertical") == 0)
        {
            SceneLoad.instance.LoadScene("MainTitle");
        }
    }
}
