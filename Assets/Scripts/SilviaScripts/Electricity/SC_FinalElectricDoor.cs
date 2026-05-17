using System;
using UnityEngine;
using UnityEngine.Events;

public class SC_FinalElectricDoor : MonoBehaviour
{
    [SerializeField] private int lightsToOpenDoor;
    private int currentLightsOn;
    
    public static System.Action ShowCredits;

    [SerializeField] private MeshRenderer lightA;
    [SerializeField] private MeshRenderer lightB;
    [SerializeField] private MeshRenderer lightC;
    [SerializeField] private MeshRenderer lightD;
    
    [SerializeField] private Material[] materials;

    [SerializeField] private UnityEvent OnOpenDoor;
    
    // Esto ahora se va a mandar por un evento desde la instancia -> OpenDoor();
    private void Awake()
    {
        lightA.material = materials[0];
        lightB.material = materials[0];
        lightC.material = materials[0];
        lightD.material = materials[0];
    }

    private void Start()
    {
        if (PuzzleLightCounter.Instance == null) return;
        if (PuzzleLightCounter.Instance.puzzleCounterA > 0) SwitchOnLightA();
        if (PuzzleLightCounter.Instance.puzzleCounterB > 0) SwitchOnLightB();
        if (PuzzleLightCounter.Instance.puzzleCounterC > 0) SwitchOnLightC();
        if (PuzzleLightCounter.Instance.puzzleCounterD > 0) SwitchOnLightD();
    }

    public void SwitchOnLightD()
    {
        lightD.material = materials[1];
    }

    public void SwitchOnLightC()
    {
        lightC.material = materials[1];
    }

    public void SwitchOnLightB()
    {
        lightB.material = materials[1];
    }

    public void SwitchOnLightA()
    {
        lightA.material = materials[1];
    }

    public void OpenDoor()
    {
        //Pausar gameplay
        //Ir a cinemática de la puerta abriéndose
        //Reanudar gameplay
        //Apagar las luces (esta ultima parte se debe hacer a oscuras)
        //Volver al gameplay normal
        Time.timeScale = 0;
        //cinemática/poner sonido en el que se oye una puerta y dice "PARECE QUE LA PUERTA SE ABRIÓ"
        Time.timeScale = 1;
        OnOpenDoor?.Invoke();
        
    }

    private void OnTriggerExit(Collider other)//cuando el jugador sale por la puerta termina el juego
    {
        if(other.CompareTag("Player"))
            ShowCredits?.Invoke();
    }

    private void OnEnable()
    {
        PuzzleLightCounter.OpenFinalDoor += OpenDoor;
    }

    // private void InstanceOnOnPuzleAComplete()
    // {
    //     Debug.Log("On puzle complete A");
    //     lightA.material = materials[1];
    //     //lightA.enabled = false;
    //     //por dios compila
    // }

    private void OnDisable()
    {
       PuzzleLightCounter.OpenFinalDoor -= OpenDoor; 
       
    }
}
