using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Actions
{
    //public static Action<PlayerController> OnPlayerJoin;
    //public static Action OnPlayerRespawn;
    //public static Action<bool, PlayerController> OnButtonPress;
    public static Action<float> OnAtaque;
    public static Action StopAtaque;
    public static Action<GameObject, float> OnBossHit;
}
