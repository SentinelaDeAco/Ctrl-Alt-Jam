using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallSFX : MonoBehaviour
{
    [SerializeField] private GameObject[] sfx;

    private void TriggerSFX(int id)
    {
        sfx[id].SetActive(true);
    }

    private void StopSFX(int id)
    {
        sfx[id].SetActive(false);
    }

    private IEnumerator SFX(int id)
    {
        sfx[id].SetActive(true);

        yield return new WaitForSeconds(0.1f);

        sfx[id].SetActive(false);
    }
}
