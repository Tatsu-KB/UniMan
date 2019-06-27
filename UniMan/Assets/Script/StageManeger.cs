using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManeger : MonoBehaviour
{
    [SerializeField] GameObject StartPos, GoalPos;
    [SerializeField] GameObject Player, Goal;
    [SerializeField] GameObject PlayerPrefab, GoalPrefab;
    [SerializeField] CameraMove Camera;
    [SerializeField] Enemy_Bee Bee;
    [SerializeField] GameObject DamegeEffect, DamegeEffectPrefab;
    [SerializeField] GameObject Effect,EffectPrefab;
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

    public void PlayerDamege(int Damege)
    {
        Player.GetComponent<Player_Move>().Damage();
    }

    public void NeedleDamage()
    {
        //Debug.Log("即死です");
        Player.GetComponent<Player_Move>().PlayerDown();
    }

    public void StageGoal()
    {
        Debug.Log("おめでとう! ゴールです!!");
        Player.GetComponent<Player_Move>().StageClear();
    }

    public void GoalPerformance()
    {
        Player.GetComponent<Player_Move>().Performance();
    }

    public void Damege(Transform P_pos)
    {
        DamegeEffectPrefab = Instantiate(DamegeEffect, P_pos);
        DamegeEffectPrefab.transform.parent = null;
    }

    public void DownEffect(Transform P_pos)
    {
        EffectPrefab =  Instantiate(Effect, P_pos);
        EffectPrefab.transform.parent = null;
    }
}
