using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamTrigger : MonoBehaviour
{
    [SerializeField] private float damage = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            Actions.OnPlayerSlam(damage);
    }
}
