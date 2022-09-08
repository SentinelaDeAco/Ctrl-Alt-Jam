using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BossIdleState : BossBaseState
{
    //Contador de Tiro
    private int shotCount = 0;

    public override void EnterState(BossStateManager boss)
    {

    }
    public override void UpdateState(BossStateManager boss)
    {

        //Debug.Log("Idle"); 
        //Debug.Log(shotCount);
        //Debug.Log("Fase " + boss.faseBoss);

        //------------Fase1--------------//
        if (boss.faseBoss == 1)
            Fase1(boss);

        //------------Fase2--------------//
        else if (boss.faseBoss == 2)
            Fase2(boss);

        //------------Fase3--------------//
        else if (boss.faseBoss == 3)
            Fase3(boss);

        //------------Morte--------------//
        else if (boss.faseBoss == 4)
            boss.SwitchState(boss.bossDieState);

        if (Input.GetKeyDown(KeyCode.L))
        {
            shotCount = 0;
            boss.SwitchState(boss.bossChangeFaseState);
        }
    }
    public override void OnCollisionEnter(BossStateManager boss, Collision collision)
    {

    }

     //----------------------------------CONTROLADORES-----------------------------------------//

    void Fase1(BossStateManager boss)
    {
        if (Input.GetKeyDown(KeyCode.K) && shotCount <= 2)
        {
            //Aumenta em Um o contador de Tiro
            shotCount += 1;

            //Debug.Log("MUDANDO DE ESTADO");
            boss.SwitchState(boss.bossShotState);
            boss.gameObject.GetComponent<Animator>().SetTrigger("Tiro");
        }
        else if (Input.GetKeyDown(KeyCode.K) && shotCount >= 3)
        {
            //Zera o contador de Tiro
            shotCount = 0;

            //Debug.Log("MUDANDO DE ESTADO");
            boss.SwitchState(boss.bossRestState);
            boss.gameObject.GetComponent<Animator>().SetTrigger("Tiro");
        }
    }

    void Fase2(BossStateManager boss)
    {
        if (Input.GetKeyDown(KeyCode.K) && shotCount <= 2)
        {
            //Aumenta em Um o contador de Tiro
            shotCount += 1;

            Debug.Log("MUDANDO DE ESTADO");
            boss.SwitchState(boss.bossShotState);
        }
        else if (Input.GetKeyDown(KeyCode.K) && shotCount == 3)
        {
            //Aumenta em Um o contador de Tiro
            shotCount = 0;

            Debug.Log("MUDANDO DE ESTADO");
            boss.SwitchState(boss.chargedShotState);
        }
    }

    void Fase3(BossStateManager boss)
    {

        if (Input.GetKeyDown(KeyCode.K) && shotCount <= 2)
        {
            //Aumenta em Um o contador de Tiro
            shotCount += 1;

            Debug.Log("MUDANDO DE ESTADO");
            boss.SwitchState(boss.bossShotState);
        }
        else if (Input.GetKeyDown(KeyCode.K) && shotCount == 3)
        {
            //Aumenta em Um o contador de Tiro
            shotCount += 1;

            Debug.Log("MUDANDO DE ESTADO");
            boss.SwitchState(boss.bossHeavyShotState);
        }

        else if (Input.GetKeyDown(KeyCode.K) && shotCount >= 4)
        {
            int randnum = Random.Range(1, 11); // 1 a 10
            if (randnum <= 3)
            {
                shotCount = 0;
                boss.SwitchState(boss.areaAttackState);
            }
            else
            {
                shotCount = 0;
                boss.SwitchState(boss.bossRestState);
            }
        }
    }
}
