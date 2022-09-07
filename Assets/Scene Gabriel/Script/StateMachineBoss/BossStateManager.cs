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

    // Start is called before the first frame update
    void Start()
    {
        //Estado Inicial Do Boss
        currentState = bossFlowerState;

        //Seta o estado quando da start
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentState);

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
