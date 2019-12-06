using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlock : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    bool DAWN;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!DAWN)
        {
            rigidbody.mass = Mathf.LerpAngle(rigidbody.mass, transform.localScale.x + 1, Time.deltaTime / 2);
        }
        else
        {
            rigidbody.mass = transform.localScale.x + 2f;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            DAWN = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DAWN = false;
        }
    }
}
