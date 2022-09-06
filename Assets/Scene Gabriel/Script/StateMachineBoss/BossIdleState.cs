
using UnityEngine;

public class BossIdleState : BossBaseState
{
    public override void EnterState(BossStateManager boss)
    {

    }

    public override void UpdateState(BossStateManager boss)
    {

        //Trocando de estado Para ShotState
        boss.SwitchState(boss.bossShotState);
    }

    public override void OnCollisionEnter(BossStateManager boss, Collision collision)
    {

    }
}
