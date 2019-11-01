using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollImage : MonoBehaviour
{ 

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-0.03f, 0, 0);
        Debug.Log(transform.position.x);
        if (transform.position.x < -16.0f)
        {
            transform.position = new Vector3(20f, 0, 0);
        }
    }
}
