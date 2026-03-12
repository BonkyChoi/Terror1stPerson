using System.Collections;
using UnityEngine;

public class SC_LightManager : MonoBehaviour
{
   public static System.Action OnSwitchOff;//activa el movimiento de los enemigos
    
    [SerializeField] private Light[] lights;

    private float startTime;

    enum LightState
    {
        Normal,
        Flicker30,
        Flicker15,
        Flicker5,
        FinalFlicker,
        Off
    }
    
    private LightState currentState;

    private void Start()
    {
        startTime = Time.time;
        currentState = LightState.Normal;

        StartCoroutine(StateTimer());
        Debug.Log("Luz encendida hasta dentro de 3 minutos");

        foreach (Light light in lights)
        {
            StartCoroutine(LightRoutine(light));
            Debug.Log("Corutina de luz iniciada para " + light.name);
        }
    }

    IEnumerator StateTimer()
    {
        yield return new WaitForSeconds(5);//cambiar a 180 para el juego final
        currentState = LightState.Flicker30;

        yield return new WaitForSeconds(5);//cambiar a 60 para el juego final
        currentState = LightState.Flicker15;

        yield return new WaitForSeconds(5);//cambiar a 60 para el juego final
        currentState = LightState.Flicker5;

        yield return new WaitForSeconds(5);//cambiar a 20 para el juego final
        currentState = LightState.FinalFlicker;
    }

    private IEnumerator LightRoutine(Light light)
    {
        while (currentState != LightState.Off)
        {
            switch (currentState)
            {
                case LightState.Normal:
                    light.enabled = true;
                    yield return null;//espera un frame antes de continuar el bucle y verificar el estado de la luz nuevamente
                    break;

                case LightState.Flicker30:
                    yield return Flick(light, 5f);//cambiar a 30
                    break;

                case LightState.Flicker15:
                    yield return Flick(light, 5f);//cambiar a 15
                    break;

                case LightState.Flicker5:
                    yield return Flick(light, 5f);
                    break;

                case LightState.FinalFlicker:

                    for (int i = 0; i < 10; i++)
                    {
                        light.enabled = !light.enabled;//cambia el estado de la luz 10 veces
                        yield return new WaitForSeconds(0.3f);
                    }

                    light.enabled = false;
                    currentState = LightState.Off;
                    OnSwitchOff?.Invoke();
                    break;
            }
        }
    }

    private IEnumerator Flick(Light light, float waitTime)//luz a apagar y tiempo de espera entre cada parpadeo
    {
        Debug.Log("Flicker en " + light.name + " con tiempo de espera de " + waitTime + " segundos");
        light.enabled = false;
        yield return new WaitForSeconds(0.3f);

        light.enabled = true;
        yield return new WaitForSeconds(waitTime);
    }

    public void SubstractTime()
    {
        startTime -= 15f;
    }

    public void OnSwitchOn()
    {
        currentState = LightState.Normal;
        StartCoroutine(StateTimer());
    }

}
