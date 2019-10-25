using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayModeVertical: MonoBehaviour
{

    new public CameraMove camera;
    public GameStart GameStart;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameStart.gameObject.activeSelf)
        {
            Invoke("Scroll", 2.0f); 
        }
    }

    void Scroll()
    {
        camera.VerticalMode = true;
    }
}
