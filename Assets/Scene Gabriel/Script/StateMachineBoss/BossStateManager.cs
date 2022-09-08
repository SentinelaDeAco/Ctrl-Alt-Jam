using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateManager : MonoBehaviour
{

    public BossBaseState currentState;
    //Stados 
    public BossChargedShot chargedShotState = new BossChargedShot();
    public BossAreaAttack areaAttackState = new BossAreaAttack();
    public BossArenaObstacleState arenaObstacleState = new BossArenaObstacleState();
    public BossDieState bossDieState = new BossDieState();
    public BossHeavyShot bossHeavyShotState = new BossHeavyShot();
    public BossIdleState bossIdleState = new BossIdleState();
    public BossRestState bossRestState = new BossRestState();
    public BossShotState bossShotState = new BossShotState();
    public BossSpawnState bossSpawnState = new BossSpawnState();
    public BossFlowerState bossFlowerState = new BossFlowerState();
    public BossChangeFaseState bossChangeFaseState = new BossChangeFaseState();

    public int faseBoss = 1;
    public bool needSwitch = false; 

    // Start is called before the first frame update
    void Start()
    {
        //Estado Inicial Do Boss
        SwitchState(bossFlowerState);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentState.GetType());

        currentState.UpdateState(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    public void SwitchState(BossBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void OnAnimationEnd()
    {
        needSwitch = true;
        SwitchState(bossIdleState);
    }
}
