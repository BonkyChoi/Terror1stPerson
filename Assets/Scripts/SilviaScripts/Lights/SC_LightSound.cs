using System;
using UnityEngine;

public class SC_LightSound : MonoBehaviour
{
    [SerializeField] private AudioSource switchOn;
    [SerializeField] private AudioSource switchOff;
    [SerializeField] private AudioSource switchpreOff;

    private void OnEnable()
    {
        SC_LightManager.OnSwitchPreOff += OnSwitchPreOff;
        SC_LightManager.OnSwitchOff += OnSwitchOff;
        SC_LightManager.OnSwitchOn += OnSwitchOn;
    }

    private void OnSwitchOn()
    {
        print("switchOn");
        switchOn.Play();
    }

    private void OnSwitchOff()
    {
        print("switchOff");
        switchOff.Play();
    }

    private void OnSwitchPreOff()
    {
        print("switchPreOff");
        switchpreOff.Play();
        
    }

    private void OnDisable()
    {
        SC_LightManager.OnSwitchPreOff -= OnSwitchPreOff;
        SC_LightManager.OnSwitchOff -= OnSwitchOff;
        SC_LightManager.OnSwitchOn -= OnSwitchOn;
    }
}
