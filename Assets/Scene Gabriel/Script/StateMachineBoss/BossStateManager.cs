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

    // Start is called before the first frame update
    void Start()
    {
        currentState = bossSpawnState;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    public void SwitchState(BossBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
