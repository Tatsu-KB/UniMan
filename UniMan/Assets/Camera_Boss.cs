using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Boss : MonoBehaviour
{
    public CameraMove CameraMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CameraMove.MinX = 258;
            CameraMove.MaxX = 258;
            CameraMove.MinY = 2;
            CameraMove.MaxY = 2;
            CameraMove.Boss = true;
        }
    }
    // X= 258
    //Y = 2
}
