using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private ParticleSystem rockVFX;
    [SerializeField] private ParticleSystem dustVFX;
    [SerializeField] private float maxHP = 100;
    private float currentHP = 100;
    private bool isVulnerable = false;
    private int phase = 1;
    private int shotCount = 0;

    private void OnEnable ()
    {
        Actions.OnBossHit += OnBossHit;
    }

    private void OnDisable()
    {
        Actions.OnBossHit -= OnBossHit;
    }

    void Update()
    {
        if (phase == 1 && currentHP < maxHP * 0.7f)
            SwitchPhases();

        if (phase == 2 && currentHP < maxHP * 0.3f)
            SwitchPhases();

        if (phase == 3 && currentHP <= 0)
            Die();
    }

    private void OnBossHit(GameObject target, float damage)
    {
        if (isVulnerable)
        {
            if (target == this.gameObject)
                currentHP -= damage;
            Debug.Log(currentHP);
        }
    }

    private void OnFightBegin()
    {
        isVulnerable = true;
    }

    private void PhaseOne()
    {
        SetRest(false);

        shotCount++;

        if (shotCount >= 3)
        {
            shotCount = 0;
            SetRest(true);
        }

        StartCoroutine(Attack(100.0f * Time.deltaTime));
    }

    private void PhaseTwo()
    {
        SetRest(false);
        SetCharge(false);

        shotCount++;

        if (shotCount >= 3)
        {
            shotCount = 0;

            int rnd = Random.Range(1, 3);

            if (rnd == 1)
                SetRest(true);
            else
                SetCharge(true);
        }

        StartCoroutine(Attack(100.0f * Time.deltaTime));
    }

    private void PhaseThree()
    {
        shotCount++;

        if (shotCount < 3)
            StartCoroutine(Attack(0.0f * Time.deltaTime));
        else if (shotCount == 3)
        {
            StartCoroutine(StrongAttack(0.0f * Time.deltaTime));
            shotCount = 0;
        }
        /*else
        {
            shotCount = 0;

            int rnd = Random.Range(1, 3);

            if (rnd == 1)
                StartCoroutine(AreaAttack(0.0f * Time.deltaTime));
        }*/
    }

    private void SetRest(bool isResting)
    {
        gameObject.GetComponent<Animator>().SetBool("Rest", isResting);
    }

    private void SetCharge(bool isCharging)
    {
        gameObject.GetComponent<Animator>().SetBool("Charge", isCharging);
    }

    private void CheckNextAction()
    {
        if (phase == 1)
            PhaseOne();

        if (phase == 2)
            PhaseTwo();

        if (phase == 3)
            PhaseThree();
    }

    private void SwitchPhases()
    {
        phase++;
        isVulnerable = false;
        gameObject.GetComponent<Animator>().SetTrigger("Roar");
    }

    private void Die()
    {
        phase++;
        gameObject.GetComponent<Animator>().SetTrigger("Death");
    }

    private void EndGroundFX()
    {
        var rock = rockVFX.emission;
        rock.enabled = false;
        var dust = dustVFX.emission;
        dust.enabled = false;
    }

    IEnumerator Attack(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        gameObject.GetComponent<Animator>().SetTrigger("Attack");
    }

    IEnumerator StrongAttack(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        gameObject.GetComponent<Animator>().SetTrigger("Strong");
    }

    IEnumerator AreaAttack(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        gameObject.GetComponent<Animator>().SetTrigger("Area");
    }
}
