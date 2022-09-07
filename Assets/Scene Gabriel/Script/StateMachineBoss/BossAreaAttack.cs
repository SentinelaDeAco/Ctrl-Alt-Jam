using UnityEngine;

public class BossAreaAttack : BossBaseState
{
    public override void EnterState(BossStateManager boss)
    {

    }

    public override void UpdateState(BossStateManager boss)
    {
        Debug.Log("AreaATK");

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
