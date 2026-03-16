using UnityEngine;

public class SC_FinalElectricDoor : MonoBehaviour
{
    [SerializeField] private int lightsToOpenDoor;
    private int currentLightsOn;
    
    public static System.Action ShowCredits;

    private void AddLightsOn()
    {
        currentLightsOn++;
        if (currentLightsOn == lightsToOpenDoor)
        {
            OpenDoor();
        }
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
        SC_PuzzlePannel.Activate1FinalLight += AddLightsOn;
    }
    private void OnDisable()
    {
        SC_PuzzlePannel.Activate1FinalLight -= AddLightsOn;
    }
}
