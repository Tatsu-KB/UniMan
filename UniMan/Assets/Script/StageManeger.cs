using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManeger : MonoBehaviour
{
    [SerializeField] GameObject StartPos;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Prefab;
    [SerializeField] CameraMove Camera;
    // Start is called before the first frame update
    void Start()
    {
        Player = Instantiate(Prefab, StartPos.transform);
        Player.transform.parent = null;
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>();
        Camera.Camera_Target();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NeedleDamage()
    {
        //Debug.Log("即死です");
        Player.GetComponent<Player_Move>().Damage();
    }
}
