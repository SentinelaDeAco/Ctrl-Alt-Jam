using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController controller = default;
    [SerializeField] private float speed = default;
    private int counter = 0;
    private bool canDash = true;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject characterModel;



    void Update()
    {
        HandleMovement();
        HandleAttack();
        OlharParaMouse();
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
}
