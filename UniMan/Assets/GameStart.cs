using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public StageManeger stage;
    void Start()
    {
        stage = GameObject.Find("StageManeger").GetComponent<StageManeger>();    
    }

    void Ready()
    {
        stage.Ready();
        gameObject.SetActive(false);
    }
}
