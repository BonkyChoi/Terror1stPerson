using System.Collections;
using UnityEngine;

public class SC_LightManager : MonoBehaviour
{
   public static System.Action OnSwitchOff;//activa el movimiento de los enemigos
   public static System.Action OnSwitchOn;//activa el movimiento de los enemigos
    
    [SerializeField] private Light[] lights;

    private float startTime;

    enum LightState
    {
        Normal,
        Flicker1,
        Flicker2,
        Flicker3,
        FinalFlicker,
        Off
    }
    
    private LightState currentState;
    
    [Header("Tiempo hasta cambiar de estado en segundos")]
    [SerializeField] private float timeToFlicker1 = 180f;
    [SerializeField] private float timeToFlicker2 = 60f;
    [SerializeField] private float timeToFlicker3 = 60f;
    [SerializeField] private float timeToFinalFlicker = 20f;
    
    [Header("Intervalos de espera al parpadear en segundos")]
    [SerializeField] private float flicker1Interval = 30f;
    [SerializeField] private float flicker2Interval = 15f;
    [SerializeField] private float flicker3Interval = 5f;
    
    [Header("Modificadores de tiempo")]
    [SerializeField] private float timeReductionOnEvent = 15f;

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
        yield return new WaitForSeconds(timeToFlicker1);
        currentState = LightState.Flicker1;

        yield return new WaitForSeconds(timeToFlicker2);
        currentState = LightState.Flicker2;

        yield return new WaitForSeconds(timeToFlicker3);
        currentState = LightState.Flicker3;

        yield return new WaitForSeconds(timeToFinalFlicker);
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

                case LightState.Flicker1:
                    yield return Flick(light, flicker1Interval);//cambiar a 30
                    break;

                case LightState.Flicker2:
                    yield return Flick(light, flicker2Interval);//cambiar a 15
                    break;

                case LightState.Flicker3:
                    yield return Flick(light, flicker3Interval);
                    break;

                case LightState.FinalFlicker:

                    for (int i = 0; i < 10; i++)
                    {
                        light.enabled = !light.enabled;//cambia el estado de la luz 10 veces
                        yield return new WaitForSeconds(0.1f);
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
        float originalIntensity = light.intensity;
        Debug.Log("Flicker en " + light.name + " con tiempo de espera de " + waitTime + " segundos");
        float randomIntensity = originalIntensity * Random.Range(0.7f, 0.9f);//baja entre un 70 y 90
        light.intensity = -1 * randomIntensity;
        yield return new WaitForSeconds(0.3f);

        light.intensity = originalIntensity;
        yield return new WaitForSeconds(waitTime);
    }

    public void SubstractTime()
    {
        Debug.Log("Han reducido el tiempo de luces");
        startTime += timeReductionOnEvent;//tiempo que le añade al contador para apagar la luz antes
    }

    public void SwitchOn()
    {
        OnSwitchOn?.Invoke();
        currentState = LightState.Normal;
        StartCoroutine(StateTimer());
        foreach (Light light in lights)
        {
            StartCoroutine(LightRoutine(light));
            Debug.Log("Corutina de luz iniciada para " + light.name);
        }
    }

    private void OnEnable()
    {
        SC_ElectricalPannel.SwitchOnTheLights += SwitchOn;
        SC_CursorPuzzle.Substract15Seconds += SubstractTime;
    }
    private void OnDisable()
    {
        SC_ElectricalPannel.SwitchOnTheLights -= SwitchOn;
        SC_CursorPuzzle.Substract15Seconds -= SubstractTime;
    }

}
