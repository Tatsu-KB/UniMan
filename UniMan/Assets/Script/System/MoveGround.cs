using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    public float MoveLimit;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Sin(Time.time) * 2.0f, transform.position.y,transform.position.z );   
    }
}
