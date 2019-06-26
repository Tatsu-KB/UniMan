using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bee : MonoBehaviour
{
    GameObject player;
    public bool EnemyFlag;
    public Renderer Renderer;
    // Start is called before the first frame update
    void Start()
    {
        EnemyFlag = false;
        Renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (EnemyFlag)
        { 
            transform.position = new Vector3(transform.position.x +  Mathf.Sin(Time.time) , transform.position.y, transform.position.z);

        }
    }

    void OnWillRenderObject()
    {
        if (Camera.current.name == "SceneCamera")
        {

        }

        if (Camera.current.name == "Main Camera")
                EnemyFlag = true;
            
    }
}
