﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManeger : MonoBehaviour
{
    [SerializeField] GameObject StartPos, GoalPos;
    [SerializeField]GameObject[] EnemyPos;
    [SerializeField] GameObject Player, Goal;
    [SerializeField] GameObject PlayerPrefab, GoalPrefab,EnemyPrefab1,EnemyPrefab2;
    [SerializeField] CameraMove Camera;
    [SerializeField] GameObject[] Bee;
    [SerializeField] GameObject DamegeEffect;
    [SerializeField] GameObject Effect;
    [SerializeField] GameObject GoalEffect;
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

        EnemyPos = GameObject.FindGameObjectsWithTag("EnemyPos");
        Debug.Log(EnemyPos.Length);
        System.Array.Resize(ref Bee, EnemyPos.Length);
        for (int No = 0; No < EnemyPos.Length; No++)
        {
           // Debug.Log(EnemyPos.Rank);
            Bee[No] = Instantiate(EnemyPrefab1, EnemyPos[No].transform);
            Bee[No].transform.parent = null;
            Bee[No].GetComponent<Enemy_Bee>().Speed = Random.Range(1, 3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log((int)Bee.Length);
    }

    public void PlayerDamege(int EnemyAtk)
    {
        Player.GetComponent<Player_Move>().Damage(EnemyAtk);
    }

    public void NeedleDamage()
    {
        Player.GetComponent<Player_Move>().PlayerDown();
    }

    public void StageGoal(Transform transform)
    {
        Debug.Log("おめでとう! ゴールです!!");
        Instantiate(GoalEffect, transform).transform.parent = null;
        Player.GetComponent<Player_Move>().StageClear();
        for (int i = 0; i < Bee.Length; i++)
        {
            Transform effect = Bee[i].transform;
            Instantiate(GoalEffect, effect.transform).transform.parent = null;
            Bee[i].GetComponent<Enemy_Bee>().Destroy();
            Debug.Log(effect.transform.position);
        }

    }

    public void GoalPerformance()
    {
        Player.GetComponent<Player_Move>().Performance();
    }

    public void Damege(Transform P_pos)
    {
        Instantiate(DamegeEffect, P_pos).transform.parent = null;
    }

    public void DownEffect(Transform P_pos)
    {
        Instantiate(Effect, P_pos).transform.parent = null;
    }

    private void Destroy()
    {
       
    }
}
