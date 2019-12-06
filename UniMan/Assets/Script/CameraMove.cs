using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] GameObject Target;
    //[SerializeField] Vector3 vector;
    [SerializeField] float MinX, MaxX, MinY, MaxY;
    public bool VerticalMode, HorizontalMode, Start = true;
    public int speed;
    // Start is called before the first frame update

    public void Camera_Target()
    {

        Target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Target && !VerticalMode && Start)
        {
            transform.position = new Vector3(Mathf.Clamp(Target.transform.position.x, MinX, MaxX), Mathf.Clamp(Target.transform.position.y, 0, 0), transform.position.z);
        }
        else
        if (Target && VerticalMode && !HorizontalMode)
        {
            if (MinY < MaxY)
            {
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, MinY, MaxY), transform.position.z);
                MinY += Time.deltaTime * speed;
            }
        }

        if (VerticalMode && HorizontalMode)
        {
            if (Target.transform.position.y  < this.transform.position.y)
            {
                transform.position = new Vector3(Mathf.Clamp(Target.transform.position.x, MinX, MaxX), transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(Mathf.Clamp(Target.transform.position.x, MinX, MaxX), Mathf.Clamp(Target.transform.position.y - 1, MinY, MaxY), transform.position.z);
            }
        }
    }


    public void PlayerHorizontalMove()
    {
        if (Target)
        {
            //transform.position = new Vector3(Mathf.Clamp(Target.transform.position.x, MinX, MaxX), transform.position.y, transform.position.z);
        }
    }
}
