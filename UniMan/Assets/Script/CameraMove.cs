﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] GameObject Target;
    [SerializeField] Vector3 vector;
    [SerializeField] float Min, Max;
    public bool VerticalMode, HorizontalMode;
    // Start is called before the first frame update

    public void Camera_Target()
    {

        Target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Target && !VerticalMode)
        {
            transform.position = new Vector3(Mathf.Clamp(Target.transform.position.x, Min, Max), transform.position.y, transform.position.z);
        }

        if (Target && VerticalMode)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(Target.transform.position.y, Min, Max), transform.position.z);
            if(Min < transform.position.y)
            {
                Min = transform.position.y;
            }
        }
    }


    public void PlayerHorizontalMove()
    {
        if (Target)
        {
            transform.position = new Vector3(Mathf.Clamp(Target.transform.position.x, Min, Max), transform.position.y, transform.position.z);
        }
    }
}
