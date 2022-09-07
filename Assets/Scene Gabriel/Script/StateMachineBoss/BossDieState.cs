
using UnityEngine;

public class BossDieState : BossBaseState
{
    public override void EnterState(BossStateManager boss)
    {

    }

    public override void UpdateState(BossStateManager boss)
    {
        Debug.Log("Morte");

    }

    public override void OnCollisionEnter(BossStateManager boss, Collision collision)
    {

    }
}
