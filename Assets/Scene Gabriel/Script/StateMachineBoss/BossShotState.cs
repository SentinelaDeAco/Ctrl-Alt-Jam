
using UnityEngine;

public class BossShotState : BossBaseState
{
    public override void EnterState(BossStateManager boss)
    {

    }

    public override void UpdateState(BossStateManager boss)
    {
        Debug.Log("Shot");

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("MUDANDO DE ESTADO");
            boss.SwitchState(boss.bossIdleState);
        }
    }

    public override void OnCollisionEnter(BossStateManager boss, Collision collision)
    {

    }
}
