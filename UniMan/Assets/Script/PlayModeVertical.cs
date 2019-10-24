using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayModeVertical: MonoBehaviour
{

    new public CameraMove camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>();
        camera.VerticalMode = true;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
