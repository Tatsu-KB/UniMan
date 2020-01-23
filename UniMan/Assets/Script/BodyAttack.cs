using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAttack : MonoBehaviour
{
    [Header("攻撃力")] public int ATK;
    StageManeger maneger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        maneger = GameObject.FindGameObjectWithTag("StageManeger").GetComponent<StageManeger>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            maneger.PlayerDamege(ATK);
        }
    }
}
