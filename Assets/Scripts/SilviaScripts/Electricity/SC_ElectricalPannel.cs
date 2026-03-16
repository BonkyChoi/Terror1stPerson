using System;
using UnityEngine;

public class SC_ElectricalPannel : MonoBehaviour
{
    //Necesito contador de luces totales para las que encender la electricidad
    //Necesito que tenga Array de luces y vaya apaándolas según se rompan
    //Necesito que en el trigger le salga la UI contexual y que al hacer el botón, si tiene el fusible en mano y es el jugador encienda la luz

    [SerializeField] private int totalLights;
    private int currentLight;
    private bool isLightOn;
    [SerializeField] private Light[] allLightsInObject;
    public static System.Action SwitchOnTheLights;

    private void Awake()
    {
        currentLight = totalLights;
        isLightOn = true;
    }

    private void OnEnable()
    {
        SC_LightManager.OnSwitchOff += LightIsOff;
        SC_LightManager.OnSwitchOn += LightIsOn;
    }
    
    
    private void OnDisable()
    {
        SC_LightManager.OnSwitchOff -= LightIsOff;
        SC_LightManager.OnSwitchOn -= LightIsOn;
    }

    private void LightIsOn()
    {
        isLightOn = true;
    }

    private void LightIsOff()
    {
        isLightOn = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))//falta poner tanto la UI como quetenga el fusible
        {
            Debug.Log("Player detected");
            if (currentLight > 0)
            {
                Debug.Log("Current light is on");
                if (Input.GetKey(KeyCode.E))
                {
                    Debug.Log("E detected");
                    if (!isLightOn)
                    {
                        Debug.Log("Light is off");
                        SwitchOnTheLights?.Invoke();
                        Debug.Log("Mando que la luz se encienda");
                        currentLight--;
                    }
                }
            }
            
        }
        
        //en el array de luces se apaga la primera
    }
}
