using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    private void OnEnable()
    {
        Actions.OnWeaponJam += HandleSimpleActivation;
    }

    private void OnDisable()
    {
        Actions.OnWeaponJam -= HandleSimpleActivation;
    }

    private void HandleSimpleActivation()
    {
        particle.Play(true);
    }
}
