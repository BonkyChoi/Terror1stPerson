using System;
using UnityEngine;

public class SC_LinternPlayer : MonoBehaviour
{
    private Light lintern;

    private void Awake()
    {
        lintern = GetComponentInChildren<Light>();
        lintern.enabled = false;
    }

    private void OnEnable()
    {
        SC_LightManager.OnSwitchOff += SwitchOnLintern;
        SC_LightManager.OnSwitchOn += SwitchOffLintern;
    }

    private void OnDisable()
    {
        
        SC_LightManager.OnSwitchOff -= SwitchOnLintern;
        SC_LightManager.OnSwitchOn -= SwitchOffLintern;
        
    }

    private void SwitchOnLintern()
    {
        lintern.enabled = true;
    }
    
    private void SwitchOffLintern()
    {
        lintern.enabled = false;
    }
}
