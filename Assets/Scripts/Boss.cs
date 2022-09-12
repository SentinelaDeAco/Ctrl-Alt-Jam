using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar = default;
    [SerializeField] private Transform targetPlayer;
    [SerializeField] private Transform shotTranform;
    [SerializeField] private ParticleSystem rockVFX;
    [SerializeField] private ParticleSystem dustVFX;
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private GameObject slamCollider;
    [SerializeField] private float maxHP = default;
    [SerializeField] private float rotationSpeed = default;
    private float currentHP = default;
    private bool isVulnerable = false;
    private bool isSleeping = true;
    private bool canLook = false;
    private bool willBreath = false;
    private int phase = 1;
    private int shotCount = 0;
    private Coroutine flashRoutine;

    private void OnEnable ()
    {
        Actions.OnBossHit += OnBossHit;
    }

    private void OnDisable()
    {
        Actions.OnBossHit -= OnBossHit;
    }

    private void Start()
    {
        currentHP = maxHP;
        healthBar.SetMaxHealth(maxHP);
    }

    void Update()
    {
        if (!isSleeping)
        {
            LookAtPlayer();

            if (phase == 1 && currentHP < maxHP * 0.7f)
                SwitchPhases();

            if (phase == 2 && currentHP < maxHP * 0.3f)
                SwitchPhases();

            if (phase == 3 && currentHP <= 0)
                Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isSleeping)
            {
                currentHP = maxHP;
                if (other is CharacterController)
                    gameObject.GetComponent<Animator>().SetTrigger("Start");
                isSleeping = false;
            }

            if (phase == 3)
                StartCoroutine(AreaAttack(0.0f * Time.deltaTime));
        }
    }

    private void OnBossHit(GameObject target, float damage)
    {
        if (isVulnerable)
        {
            Flash();

            if (target == this.gameObject)
                currentHP -= damage;

            healthBar.SetHealth(currentHP);
        }
    }

    private void Flash()
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(DamageFlash(0.125f));
    }

    private void OnFightBegin()
    {
        isVulnerable = true;
    }

    private void PhaseOne()
    {
        ResetAnimationBools();

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
        ResetAnimationBools();

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

        StartCoroutine(Attack(50.0f * Time.deltaTime));
    }

    private void PhaseThree()
    {
        ResetAnimationBools();

        shotCount++;

        if (shotCount < 3)
            StartCoroutine(Attack(0.0f * Time.deltaTime));
        else if (shotCount == 3)
        {
            StartCoroutine(StrongAttack(0.0f * Time.deltaTime));
            shotCount = 0;
        }
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

        if (phase == 3 && !willBreath)
            PhaseThree();
    }

    private void SwitchPhases()
    {
        shotCount = 0;
        phase++;
        isVulnerable = false;
        gameObject.GetComponent<Animator>().SetTrigger("Roar");
    }

    private void BeginSlam()
    {
        slamCollider.SetActive(true);
    }

    private void EndSlam()
    {
        slamCollider.SetActive(false);
    }

    private void Die()
    {
        phase++;
        isVulnerable = false;
        gameObject.GetComponent<Animator>().SetTrigger("Death");
        Actions.DeactivateBreath();
        Actions.OnBossKilled();
    }

    private void PlayGroundFX()
    {
        var rock = rockVFX.emission;
        rock.enabled = true;
        var dust = dustVFX.emission;
        dust.enabled = true;
    }

    private void EndGroundFX()
    {
        var rock = rockVFX.emission;
        rock.enabled = false;
        var dust = dustVFX.emission;
        dust.enabled = false;
    }

    private void LookAtPlayer()
    {
        if (canLook)
        {
            Quaternion toRotation = Quaternion.LookRotation(targetPlayer.position, Vector3.up);
            this.gameObject.transform.rotation = Quaternion.RotateTowards(this.gameObject.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void CanLookTrue()
    {
        canLook = true;
    }

    private void CanLookFalse()
    {
        canLook = false;
    }

    private void ResetAnimationBools()
    {
        if (gameObject.GetComponent<Animator>().GetBool("Rest"))
            SetRest(false);
        if (gameObject.GetComponent<Animator>().GetBool("Charge"))
            SetCharge(false);
    }

    private void SpawnShot()
    {
        Instantiate(shotPrefab, shotTranform.position, this.gameObject.transform.rotation);
    }

    private void ActivateBreath()
    {
        Actions.ActivateBreath();
    }

    private void DeactivateBreath()
    {
        Actions.DeactivateBreath();
    }

    private void BoolAreaFalse()
    {
        willBreath = false;
        gameObject.GetComponent<Animator>().SetBool("Area", false);
    }

    private IEnumerator Attack(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        gameObject.GetComponent<Animator>().SetTrigger("Attack");
    }

    private IEnumerator StrongAttack(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        gameObject.GetComponent<Animator>().SetTrigger("Strong");
    }

    private IEnumerator AreaAttack(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        willBreath = true;
        gameObject.GetComponent<Animator>().SetBool("Area", true);
    }

    private IEnumerator DamageFlash(float delayTime)
    {
        transform.Find("boss1").GetComponent<Renderer>().material.SetFloat("_intencidade_Fresnel", 2f);

        yield return new WaitForSeconds(delayTime);
        
        transform.Find("boss1").GetComponent<Renderer>().material.SetFloat("_intencidade_Fresnel", 0f);

        flashRoutine = null;
    }
}
