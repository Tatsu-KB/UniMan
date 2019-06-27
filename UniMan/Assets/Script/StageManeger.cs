using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManeger : MonoBehaviour
{
    [SerializeField] GameObject StartPos, GoalPos;
    [SerializeField] GameObject Player, Goal;
    [SerializeField] GameObject PlayerPrefab, GoalPrefab;
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
        Bee = GameObject.FindGameObjectsWithTag("Bee");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log((int)Bee.Length);
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
}
