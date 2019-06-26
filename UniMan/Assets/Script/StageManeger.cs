using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManeger : MonoBehaviour
{
    [SerializeField] GameObject StartPos,GoalPos;
    [SerializeField] GameObject Player,Goal;
    [SerializeField] GameObject PlayerPrefab,GoalPrefab;
    [SerializeField] CameraMove Camera;
    // Start is called before the first frame update
    void Start()
    {
        Player = Instantiate(PlayerPrefab, StartPos.transform);
        Player.transform.parent = null;
        Goal = Instantiate(GoalPrefab, GoalPos.transform);
        Goal.tag = GoalPos.tag;
        Goal.transform.parent = null;
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

    public void StageGoal()
    {
        Debug.Log("おめでとう! ゴールです!!");
        Player.GetComponent<Player_Move>().StageClear();
    }
}
