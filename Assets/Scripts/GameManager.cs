using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        Actions.OnPlayerDeath += OnPlayerDeath;
        Actions.OnBossKilled += OnBossKilled;
    }
    private void OnDisable()
    {
        Actions.OnPlayerDeath -= OnPlayerDeath;
        Actions.OnBossKilled -= OnBossKilled;
    }

    private void OnPlayerDeath()
    {
        StartCoroutine(MoveToGameOverScene());
    }

    private void OnBossKilled()
    {
        StartCoroutine(MoveToEndingScene());
    }

    private IEnumerator MoveToGameOverScene()
    {
        yield return new WaitForSeconds(200.0f);

        SceneManager.LoadScene("GameOver");
    }

    private IEnumerator MoveToEndingScene()
    {
        yield return new WaitForSeconds(200.0f);

        SceneManager.LoadScene("TheEnd");
    }
}
