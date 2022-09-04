using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEnabler : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    private float damage;

    private void OnEnable()
    {
        Actions.OnAtaque += HandleActivation;
        Actions.StopAtaque += HandleDeactivation;
    }

    private void OnDisable()
    {
        Actions.OnAtaque -= HandleActivation;
        Actions.StopAtaque -= HandleDeactivation;
    }

    private void Start()
    {
        HandleDeactivation();
    }

    private void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Actions.OnBossHit(other, damage);
    }

    private void HandleActivation(float damage)
    {
        var em = particle.emission;
        em.enabled = true;
        this.damage = damage;
    }

    private void HandleDeactivation()
    {
        var em = particle.emission;
        em.enabled = false;
        this.damage = 0f;
    }
}
