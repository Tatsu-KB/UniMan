using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] GameObject Target;
    [SerializeField] Vector3 vector;
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (Mathf.Clamp(Target.transform.position.x, 0, 30),transform.position.y,transform.position.z);
    }
}
