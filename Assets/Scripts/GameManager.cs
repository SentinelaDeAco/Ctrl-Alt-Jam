using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private int modID;

    private void OnEnable()
    {
        //Actions.OnModSwitch += HandleSwitchMods;
    }
    private void OnDisable()
    {
        //Actions.OnModSwitch -= HandleSwitchMods;
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void HandleSwitchMods(int newID)
    {
        
    }
}
