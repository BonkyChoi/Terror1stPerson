using System;
using UnityEngine;

public class SC_PuzzlePannel : MonoBehaviour
{
    //tiene un cursor y una zona roja si le pulsas en la zona roja 3 veces permites abrir una puerta
    //por cada vez que falles a la hora de pulsar te quita tiempo de luces (15 segundos)
    //este enciende le dice al canvas del cursor que se encienda

    public static System.Action ShowPuzzlePannel;
    
    [SerializeField] private int totalTimesToSuccess;
    private int currentTimesToSuccess;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")&& currentTimesToSuccess < totalTimesToSuccess && Input.GetKey(KeyCode.E))
        {
            ShowPuzzlePannel?.Invoke();
        }
        else if (other.CompareTag("Player") && currentTimesToSuccess == totalTimesToSuccess)
        {
            //en vez de invocar el evento lo guarda en una instancia 
        }
    }

    private void OnEnable()
    {
        SC_CursorPuzzle.AddSuccess += AddSuccess;
    }
    
    private void OnDisable()
    {
        SC_CursorPuzzle.AddSuccess -= AddSuccess;
    }

    private void AddSuccess()
    {
        currentTimesToSuccess++;
    }
}
