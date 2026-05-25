using System;
using Unity.VisualScripting;
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
    private bool canInteract;
    public static System.Action SwitchOnTheLights;

    [SerializeField] private GameObject playerRightHand;
    [SerializeField] private GameObject playerLeftHand;

    private void Awake()
    {
        currentLight = totalLights;
        isLightOn = true;
    }

    private void OnEnable()
    {
        SC_LightManager.OnSwitchOff += LightIsOff;
        SC_LightManager.OnSwitchOn += LightIsOn;
        SC_PlayerBrain.OnInteract += OnInteract;
        SC_PlayerBrain.OnDesinteract += OnDesinteract;
    }

    private void OnDesinteract()
    {
        canInteract = false;
    }

    private void OnInteract()
    {
        canInteract = true;
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
            print("Es player");
            if (currentLight > 0)
            {
                print("lasLuceSon mayores a 0");
                if (canInteract)
                {
                    print("playerHasInteracted");
                    if (!isLightOn)
                    {
                        print("light is off");
                        GameObject fuse = GetFuseInHand();

                        if (fuse != null)
                        {
                            print("Tiene un fusible en la mano");

                            SwitchOnTheLights?.Invoke();

                            currentLight--;

                            Destroy(fuse);

                            print("enciendo luz");
                        }
                    }
                }
            }
            
        }
        
        //en el array de luces se apaga la primera
    }
    private GameObject GetFuseInHand()
    {
        // Mano derecha
        foreach (Transform child in playerRightHand.transform)
        {
            if (child.CompareTag("Fusible"))
            {
                return child.gameObject;
            }
        }

        // Mano izquierda
        foreach (Transform child in playerLeftHand.transform)
        {
            if (child.CompareTag("Fusible"))
            {
                return child.gameObject;
            }
        }

        return null;
    }
}
