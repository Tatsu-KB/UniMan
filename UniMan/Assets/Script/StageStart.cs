using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStart : MonoBehaviour
{
    StageManeger stageManeger;
    // Start is called before the first frame update
    void Start()
    {
        stageManeger = GameObject.FindGameObjectWithTag("StageManeger").GetComponent<StageManeger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
