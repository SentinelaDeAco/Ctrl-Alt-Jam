using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerController == null)
            return;

        Vector3 playerPosition = playerController.transform.position;
        transform.position = playerPosition + Vector3.up * 12f + Vector3.back * 4f;
    }
}
