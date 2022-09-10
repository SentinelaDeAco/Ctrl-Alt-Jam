using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] private float damage = 0.02f;
    private bool isColliding = false;
    private GameObject target = default;

    /*private void OnTriggerStay(Collider other)
    {
        
    }*/

    private void OnTriggerEnter(Collider other)
    {
        target = other.gameObject;
        isColliding = true;
    }

    private void OnTriggerExit(Collider other)
    {
        target = null;
        isColliding = false;
    }

    private void Update()
    {
        DealDamage();
    }

    private void DealDamage()
    {
        if (isColliding)
        {
            if (target.tag == "Boss")
                Actions.OnBossHit(target, damage);
            if (target.tag == "Player")
                Actions.OnPlayerHit(damage);
        }
    }

    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
