using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Actions
{
    public static Action<int, float> OnAtaque;
    public static Action StopAtaque;
    public static Action<GameObject, float> OnBossHit;
    public static Action<float> OnPlayerHit;
    public static Action<float> OnPlayerSlam;
    public static Action OnPlayerDeath;
    public static Action ActivateBreath;
    public static Action DeactivateBreath;
}
