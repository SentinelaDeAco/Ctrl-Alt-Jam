using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private float hp = 100;

    void OnEnable ()
    {
        Actions.OnBossHit += OnBossHit;
    }

    void OnDisable()
    {
        Actions.OnBossHit -= OnBossHit;
    }

    void Update()
    {
        
    }

    private void OnBossHit(GameObject target, float damage)
    {
        if (target == this.gameObject)
            hp -= damage;
        Debug.Log(hp);
    }
}
