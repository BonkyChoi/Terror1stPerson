using UnityEngine;

public class SC_FinalElectricDoor : MonoBehaviour
{
    [SerializeField] private int lightsToOpenDoor;
    private int currentLightsOn;
    
    public static System.Action ShowCredits;

    [SerializeField] private Light lightA;
    [SerializeField] private Light lightB;
    [SerializeField] private Light lightC;
    [SerializeField] private Light lightD;
    
    // Esto ahora se va a mandar por un evento desde la instancia -> OpenDoor();

    private void Start()
    {
        if (PuzzleLightCounter.Instance.puzzleCounterA > 0) SwitchOnLightA();
        if (PuzzleLightCounter.Instance.puzzleCounterB > 0) SwitchOnLightB();
        if (PuzzleLightCounter.Instance.puzzleCounterC > 0) SwitchOnLightC();
        if (PuzzleLightCounter.Instance.puzzleCounterD > 0) SwitchOnLightD();
    }

    private void SwitchOnLightD()
    {
        lightD.intensity = 1;
    }

    private void SwitchOnLightC()
    {
        lightC.intensity = 1;
    }

    private void SwitchOnLightB()
    {
        lightB.intensity = 1;
    }

    private void SwitchOnLightA()
    {
        lightA.intensity = 1;
    }

    private void OpenDoor()
    {
        //Pausar gameplay
        //Ir a cinemática de la puerta abriéndose
        //Apagar las luces
        //Volver al gameplay normal
    }

    private void OnTriggerExit(Collider other)//cuando el jugador sale por la puerta termina el juego
    {
        if(other.CompareTag("Player"))
            ShowCredits?.Invoke();
    }

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
}
