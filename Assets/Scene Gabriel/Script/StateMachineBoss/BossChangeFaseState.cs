using UnityEngine;

public class BossChangeFaseState : BossBaseState
{
    public override void EnterState(BossStateManager boss)
    {

    }

    public override void UpdateState(BossStateManager boss)
    {

        Debug.Log("Change Fase Stade");

        if (Input.GetKeyDown(KeyCode.K))
        {

            boss.faseBoss += 1;

            //-----------Morte Matada-------------//
            if(boss.faseBoss == 4)
                boss.SwitchState(boss.bossDieState);
            //------------------------------------//


            Debug.Log("MUDANDO DE ESTADO");
            boss.SwitchState(boss.bossIdleState);
        }
    }

    public override void OnCollisionEnter(BossStateManager boss, Collision collision)
    {

    }
}
