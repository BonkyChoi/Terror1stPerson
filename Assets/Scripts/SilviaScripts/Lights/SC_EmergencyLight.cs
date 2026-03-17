using UnityEngine;

public class SC_EmergencyLight : MonoBehaviour
{
    private Light light;
    [SerializeField] private Material[] materials;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        light = GetComponentInChildren<Light>();
        light.enabled = false;
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = materials[0];
        
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
        meshRenderer.material = materials[0];
        light.enabled = false;
    }

    private void SwitchOnEmergencyLight()
    {
        meshRenderer.material = materials[1];
       light.enabled = true;
    }
}
