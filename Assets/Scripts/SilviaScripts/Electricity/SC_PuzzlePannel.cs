using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SC_PuzzlePannel : MonoBehaviour
{
    
    
    [SerializeField] private SC_CursorPuzzle cursorPuzzle;

    [SerializeField] private UnityEvent onPuzzleCompleted;
    [SerializeField] private UnityEvent onPuzzleInteract;

    

    private bool CanSuccess;
    private bool canInteract;
    private int timesPlayed;
    
    
    private bool puzzleOpened;

    private void Awake()
    {
        CanSuccess = false;
        //canInteract = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        Debug.Log("------------ISPLATYER");

        if (CanSuccess) return;
        
        Debug.Log("------------!CANSUCCESS");
        
        print(canInteract);
        print(puzzleOpened);

        if (canInteract && !puzzleOpened)
        {
            puzzleOpened = true;

            onPuzzleInteract?.Invoke();

            Debug.Log("Puzzle Opened");
        }
    }
    public void ResetPuzzle()
    {
        puzzleOpened = false;
        canInteract = false;
    }

    /*if (!other.CompareTag("Player")) return;

        if (!CanSuccess)
        {
            //Quiero que solo si le he dado al boton de interactuar dentro de esto pueda hacerlo
            if (canInteract && timesPlayed < 1)
            {
                //SC_GameManager.Instance.OpenUI();
                //OpenUI();
                //
                onPuzzleInteract?.Invoke();
                Debug.Log("OnInteractionStartedHola");

                //no solo hay que ligarlo al puzle, debes abrir la UI del game manager
                //ShowPuzzlePannel?.Invoke(); en el cursor pannel

            }

            else
            {
                //SC_GameManager.Instance.CloseUI();
                //CloseUI();
                //ReactivateUIButton?.Invoke(); ESTA EN EL CURSOR PUZLE

                onPuzzleCompleted?.Invoke();
                canInteract = false;
                //tambien debes cerrar la ui y reactivar el botón de sta
            }
        }*/
    

    private void OnEnable()
    {
        //cursorPuzzle.AddSuccess += CursorSendSuccess;
        SC_PlayerBrain.OnInteract += OnInteract;
        //SC_PlayerBrain.OnDesinteract += OnStopInteract;
    }

    // private void CursorPuzzleOnAddSuccess()
    // {
    //     CanSuccess++;
    // }

    private void OnDisable()
    {
        //cursorPuzzle.AddSuccess -= CursorSendSuccess;
        SC_PlayerBrain.OnInteract -= OnInteract;
        //SC_PlayerBrain.OnDesinteract += OnStopInteract;
    }

    public void CursorSendSuccess()
    {
        CanSuccess = true;
    }

    private void OnInteract()
    {
        canInteract = true;

        StartCoroutine(Stopinteraction());
    }

    private IEnumerator Stopinteraction()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return null;
        }
        
        canInteract = false;
    }

    // private void OnStopInteract()
    // {
    //     for (int i = 0; i < 100; i++)
    //     {
    //         Debug.Log("------------StopInteract");
    //     }
    //     canInteract = false;
    // }

//     public void CantInteract()
//     {
//         CanInteract = false;
//     }
// //
//     private void AddSuccess()
//     {
//         currentTimesToSuccess++;
//     }
}