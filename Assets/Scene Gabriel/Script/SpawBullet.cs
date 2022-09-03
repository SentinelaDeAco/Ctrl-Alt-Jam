using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawBullet : MonoBehaviour
{
    [SerializeField] private GameObject projetil;
    [SerializeField] private Transform cria_bala_tranform;

    private void OnEnable()
    {
        Actions.OnAtaque += Spawner;
    }

    private void OnDisable()
    {
        Actions.OnAtaque -= Spawner;
    }

    void Spawner()
    {
        Instantiate(projetil, cria_bala_tranform.position, cria_bala_tranform.rotation);
        
    }

}