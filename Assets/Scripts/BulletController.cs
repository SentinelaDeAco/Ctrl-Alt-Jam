using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject owner;

    void Start()
    {
        transform.position = owner.transform.position;
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {

    }
}
