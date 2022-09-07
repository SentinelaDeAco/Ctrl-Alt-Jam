
using UnityEngine;

public class BossHeavyShot : BossBaseState
{
    public override void EnterState(BossStateManager boss)
    {

    }

    public override void UpdateState(BossStateManager boss)
    {
        Debug.Log("HeavyShot");

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("MUDANDO DE ESTADO");
            boss.SwitchState(boss.bossRestState);
        }
    }

    public override void OnCollisionEnter(BossStateManager boss, Collision collision)
    {

    }
}
