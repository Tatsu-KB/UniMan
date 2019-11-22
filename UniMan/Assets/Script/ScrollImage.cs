using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollImage : MonoBehaviour
{ 

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-0.03f, 0, 0);
        if (transform.position.x < -16.0f)
        {
            transform.position = new Vector3(19f, 0, 0);
        }
    }
}
