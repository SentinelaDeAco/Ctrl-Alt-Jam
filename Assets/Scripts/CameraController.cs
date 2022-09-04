using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    void Start()
    {

    }

    void LateUpdate()
    {
        if (playerController == null)
            playerController = FindObjectOfType<PlayerController>();

        Vector3 playerPosition = playerController.transform.position;
        transform.position = playerPosition + Vector3.up * 12f + Vector3.back * 4f;
    }
}
