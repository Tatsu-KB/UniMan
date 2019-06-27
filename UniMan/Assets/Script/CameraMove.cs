using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] GameObject Target;
    [SerializeField] Vector3 vector;
    [SerializeField] float Min, Max;
    // Start is called before the first frame update

    public void Camera_Target()
    {
        Debug.Log(Target);
        Target = GameObject.FindGameObjectWithTag("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (Mathf.Clamp(Target.transform.position.x, Min, Max),transform.position.y,transform.position.z);
    }
}
