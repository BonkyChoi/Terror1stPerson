using UnityEngine;

public class SC_EmergencyLight : MonoBehaviour
{
    private Light light;

    private void Awake()
    {
        light = GetComponentInChildren<Light>();
        light.enabled = false;
    }

    private void OnEnable()
    {
        SC_LightManager.OnSwitchOff += SwitchOnEmergencyLight;
        SC_LightManager.OnSwitchOn += SwitchOffEmergencyLight;
    }
    
    private void OnDisable()
    {
        SC_LightManager.OnSwitchOff -= SwitchOnEmergencyLight;
        SC_LightManager.OnSwitchOn -= SwitchOffEmergencyLight;
    }

    private void SwitchOffEmergencyLight()
    {
        light.enabled = false;
    }

    private void SwitchOnEmergencyLight()
    {
       light.enabled = true;
    }
}
