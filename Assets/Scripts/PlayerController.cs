using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController controller = default;
    [SerializeField] private float speed = default;
    private int counter = 0;
    private bool canDash = true;

    void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        if (canDash) HandleDash();

        controller.Move(move * speed * Time.deltaTime);
    }

    private void HandleDash()
    {
        if (Input.GetKeyDown("space"))
        {
            speed = 25;
            counter = 0;
            //canDash = false;
        }

        if (counter > 50)
            speed = 10;
        else counter++;
    }

    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Actions.OnAtaque();
        }
    }
}
