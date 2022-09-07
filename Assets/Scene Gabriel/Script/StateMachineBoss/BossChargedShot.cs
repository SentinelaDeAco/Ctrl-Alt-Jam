
using UnityEngine;

public class BossChargedShot : BossBaseState
{
    public override void EnterState(BossStateManager boss)
    {

    }

    public override void UpdateState(BossStateManager boss)
    {
        Debug.Log("Charging");

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("MUDANDO DE ESTADO");
            boss.SwitchState(boss.bossHeavyShotState);
        }
    }

    public override void OnCollisionEnter(BossStateManager boss, Collision collision)
    {

    }


}
