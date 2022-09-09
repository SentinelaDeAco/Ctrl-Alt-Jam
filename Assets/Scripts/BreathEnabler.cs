using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathEnabler : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float damage = 0.1f;

    private void OnEnable()
    {
        Actions.ActivateBreath += HandleActivation;
        Actions.DeactivateBreath += HandleDeactivation;
    }

    private void OnDisable()
    {
        Actions.ActivateBreath -= HandleActivation;
        Actions.DeactivateBreath -= HandleDeactivation;
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
        Actions.OnPlayerHit(damage);
    }

    private void HandleActivation()
    {
        var em = particle.emission;
        em.enabled = true;
    }

    private void HandleDeactivation()
    {
        var em = particle.emission;
        em.enabled = false;
    }
}
