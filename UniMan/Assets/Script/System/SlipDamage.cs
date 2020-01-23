using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipDamage : MonoBehaviour
{
    [SerializeField] StageManeger StageManeger;
    [SerializeField] GameObject Target;

    // Start is called before the first frame update
    void Awake()
    {
        StageManeger = GameObject.FindGameObjectWithTag("StageManeger").GetComponent<StageManeger>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Target)
        {
            Target = GameObject.FindGameObjectWithTag("Player");
        }
        if (Target.transform.position.y <= this.transform.position.y + 3.0f)
        {
            StageManeger.SlipDamage();
        }
    }
    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            StageManeger.SlipDamage();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StageManeger.SlipDamage();
        }
    }
    */
}
