
using UnityEngine;

public class BossFlowerState : BossBaseState
{
    public override void EnterState(BossStateManager boss)
    {

    }

    public override void UpdateState(BossStateManager boss)
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("MUDANDO DE ESTADO");
            boss.SwitchState(boss.bossSpawnState);
        }
    }

    public override void OnCollisionEnter(BossStateManager boss, Collision collision)
    {

    }
}
