using System;
using UnityEngine;

public class SC_PuzzlePannel : MonoBehaviour
{
    //tiene un cursor y una zona roja si le pulsas en la zona roja 3 veces permites abrir una puerta
    //por cada vez que falles a la hora de pulsar te quita tiempo de luces (15 segundos)

    public static System.Action ShowPuzzlePannel;
    public static System.Action Activate1FinalLight;
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
            Activate1FinalLight?.Invoke();
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
