using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    [SerializeField] StageManeger StageManeger;
    // Start is called before the first frame update
    void Start()
    {
        StageManeger = GameObject.FindGameObjectWithTag("StageManeger").GetComponent<StageManeger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Player")
        {
            StageManeger.NeedleDamage();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StageManeger.NeedleDamage();
        }
    }
}
