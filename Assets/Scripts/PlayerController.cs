using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController controller = default;
    [SerializeField] private float speed = default;
    [SerializeField] private float rotationSpeed = default;
    private int counter = 0;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject characterModel;
    [SerializeField] private GameObject characterLegs;
    [SerializeField] private float damage = 0.1f;
    [SerializeField] private float maxHP = 10f;
    private float currentHP = 10f;
    [SerializeField] private int maxModsTEST = 1;
    private int currentMod = 0; //0 = Normal; 1 = Flamethrower; 2 = Laser; 3 = Rune
    [SerializeField] private GameObject baseWeapon = default;
    [SerializeField] private GameObject mod1 = default;
    //[SerializeField] private GameObject mod2 = default;
    //[SerializeField] private GameObject mod3 = default;
    [SerializeField] private HealthBar healthBar = default;
    [SerializeField] private GameObject lava = default;
    [SerializeField] private Transform lavaPos = default;
    private bool isAlive = true;
    private bool isJamming = false;
    private bool jamBlock = false;
    private bool canUgh = true;
    [SerializeField] private GameObject lavaParticle;
    [SerializeField] private GameObject[] sfx;

    void Start()
    {
        currentHP = maxHP;
        healthBar.SetMaxHealth(maxHP);
        isAlive = true;
    }

    private void OnEnable()
    {
        Actions.OnPlayerHit += HandleHit;
    }

    private void OnDisable()
    {
        Actions.OnPlayerHit -= HandleHit;
    }

    void Update()
    {
        if (isAlive)
        {
            HandleMovement();
            HandleAttack();
            HandleDeath();
            OlharParaMouse();
            SwitchMod();

            if (currentMod != 0)
                TryRandomNumber();
        }
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        move = move.normalized;

        //HandleDash();

        controller.Move(move * speed * Time.deltaTime);

        if (move != new Vector3(0f, 0f, 0f))
            SwitchAnimatorBool("isRunning", true);
        else
            SwitchAnimatorBool("isRunning", false);

        if (move != Vector3.zero)
        {
            RotateLegs(x, z);
        }
    }

    private void HandleDash()
    {
        if (Input.GetKeyDown("space"))
        {
            speed = 25;
            counter = 0;
        }

        if (counter > 50)
            speed = 10;
        else counter++;
    }

    private void HandleAttack()
    {
        if (!isJamming)
        {
            if (Input.GetMouseButtonDown(0))
                Actions.OnAtaque(currentMod, damage);

            else if (Input.GetMouseButtonUp(0))
                Actions.StopAtaque();
        }
        else
            HandleJamming();
    }

    private void HandleJamming()
    {
        if (currentMod == 1)
        {
            Actions.StopAtaque();

            if (Input.GetMouseButton(0) && !jamBlock)
            {
                jamBlock = true;

                lavaParticle.SetActive(true);

                StartCoroutine(DelayLava(80.0f * Time.deltaTime));
                StartCoroutine(DelayUnjamming(300.0f * Time.deltaTime));
            }
        }
    }

    private void HandleHit(float damage)
    {
        if (isAlive)
        {
            currentHP -= damage;
            if (currentHP < 0)
                currentHP = 0;

            if (canUgh)
            {
                canUgh = false;
                sfx[0].SetActive(true);
                StartCoroutine(HurtCooldown(50.0f * Time.deltaTime));
            }
            healthBar.SetHealth(currentHP);
        }
    }

    private void HandleDeath()
    {
        if (currentHP <= 0)
        {
            isAlive = false;

            Actions.OnPlayerDeath();

            this.gameObject.transform.Find("MagoTop").GetComponent<Animator>().SetTrigger("Death");
            this.gameObject.transform.Find("MagoLegs").GetComponent<Animator>().SetTrigger("Death");
        }
    }

    private void OlharParaMouse()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        //faz um plano infinito.
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            //desenha linha da camera ate o chao onde o mouse aponta.
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

            characterModel.transform.LookAt(new Vector3(pointToLook.x,characterModel.transform.position.y, pointToLook.z));
        }
    }

    private void SwitchMod()
    {
        if (Input.GetKeyDown("e"))
        {
            currentMod++;
            if (currentMod > maxModsTEST)
                currentMod = 0;

            DeactivateWeapons();

            switch (currentMod)
            {
                case 0:
                    baseWeapon.SetActive(true);
                    break;
                case 1:
                    mod1.SetActive(true);
                    break;
                /*case 2:
                    mod2.SetActive(true);
                    break;
                case 3:
                    mod3.SetActive(true);
                    break;*/
                default:
                    break;
            }
        }
    }

    private void TryRandomNumber()
    {
        if (!isJamming)
        {
            int num = Random.Range(1, 3001);

            if (num == 1) 
            { 
                isJamming = true;
                sfx[1].SetActive(true);
                sfx[1].SetActive(false);
            }
        }
    }

    private void SwitchAnimatorBool(string trigger, bool state)
    {
        this.gameObject.transform.Find("MagoTop").GetComponent<Animator>().SetBool(trigger, state);
        this.gameObject.transform.Find("MagoLegs").GetComponent<Animator>().SetBool(trigger, state);
    }

    private void RotateLegs(float x, float z)
    {
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(x, characterLegs.transform.position.y, z), Vector3.up);
        characterLegs.transform.rotation = Quaternion.RotateTowards(characterLegs.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }

    private void DeactivateWeapons()
    {
        baseWeapon.SetActive(false);
        mod1.SetActive(false);
        //mod2.SetActive(false);
        //mod3.SetActive(false);
    }

    IEnumerator DelayLava(float delayTime)
    {
        Vector3 fixedPos = lavaPos.position;
        Quaternion fixedRot = lavaPos.rotation;

        yield return new WaitForSeconds(delayTime);

        Instantiate(lava, fixedPos, fixedRot);
    }

    IEnumerator DelayUnjamming(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        isJamming = false;
        jamBlock = false;

        lavaParticle.SetActive(false);
    }

    IEnumerator HurtCooldown(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        sfx[0].SetActive(false);
        canUgh = true;
    }
}
