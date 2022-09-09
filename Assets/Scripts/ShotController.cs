using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    [SerializeField] private float speed = 1000f;
    [SerializeField] private float damage = 1f;
    private GameObject instance;

    void Start()
    {
        if (instance == null)
            instance = this.gameObject;

        instance.GetComponent<Rigidbody>().AddForce(instance.transform.forward * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Actions.OnPlayerHit(damage);
            Object.Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Environment")
            Object.Destroy(this.gameObject);
    }
}
