using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManeger : MonoBehaviour
{
    [SerializeField] GameObject StartPos, GoalPos;
    [SerializeField]GameObject[] EnemyPos_Bee,EnemyPos_Piranha;
    [SerializeField] GameObject Player, Goal;
    [SerializeField] GameObject PlayerPrefab, GoalPrefab,EnemyPrefab1,EnemyPrefab2;
    [SerializeField] CameraMove Camera;
    [SerializeField] GameObject[] Bee,Piranha;
    [SerializeField] GameObject DamegeEffect,Effect,GoalEffect,EnemyEffect;
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

        EnemyPos_Bee = GameObject.FindGameObjectsWithTag("EnemyPos1");
        EnemyPos_Piranha = GameObject.FindGameObjectsWithTag("EnemyPos2");
        Debug.Log(EnemyPos_Bee.Length);
        Debug.Log(EnemyPos_Piranha.Length);

        System.Array.Resize(ref Bee, EnemyPos_Bee.Length);
        System.Array.Resize(ref Piranha, EnemyPos_Piranha.Length);

        for (int No1 = 0; No1 < EnemyPos_Bee.Length; No1++)
        {
            Bee[No1] = Instantiate(EnemyPrefab1, EnemyPos_Bee[No1].transform);
            Bee[No1].transform.parent = null;
            Bee[No1].GetComponent<Enemy_Bee>().Speed = 2;
        }

        for (int No2 = 0; No2 < EnemyPos_Piranha.Length; No2++)
        {
            Piranha[No2] = Instantiate(EnemyPrefab2, EnemyPos_Piranha[No2].transform);
            Piranha[No2].transform.parent = null;
            //Piranha[No2] = Instantiate(EnemyPrefab2, EnemyPos_Piranha[No2].transform);
            //Piranha[No2].transform.parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log((int)Bee.Length);
    }

    public void EnemyDamage(GameObject Enemy, int ATK,Transform Pos)
    {
        Debug.Log(Enemy.name);
        if(Enemy.tag == "Bee")Enemy.GetComponent<Enemy_Bee>().Damage(ATK);
        if (Enemy.tag == "Piranha") Enemy.GetComponent<Enemy_Piranha>().Damage(ATK);

        Instantiate(DamegeEffect, Pos).transform.parent = null;
    }
    public void EnemyDown(Transform Enemy_pos)
    {
        Instantiate(EnemyEffect, Enemy_pos).transform.parent = null;
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
            if (Bee[i] != null)
            {
                Transform effect_Bee = Bee[i].transform;
                Instantiate(EnemyEffect, effect_Bee).transform.parent = null;
                Bee[i].GetComponent<Enemy_Bee>().Destroy();
            }
        }
        for (int i = 0; i < Piranha.Length; i++)
        {
            if (Piranha[i] != null)
            {
                Transform effect_Piranha = Piranha[i].transform;
                Instantiate(EnemyEffect, effect_Piranha).transform.parent = null;
                Piranha[i].GetComponent<Enemy_Piranha>().Destroy();
            }
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

        for (int i = 0; i < Piranha.Length;i++)
            if(Piranha[i] != null) Piranha[i].GetComponent<Enemy_Piranha>().ActiveFalse();
    }

    public void BreakEffect(Transform Pos)
    {
        Instantiate(DamegeEffect, Pos).transform.parent = null;
    }


}
