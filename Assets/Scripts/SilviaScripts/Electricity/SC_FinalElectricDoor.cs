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
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            ShowCredits?.Invoke();
    }
}
