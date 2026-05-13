using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SC_PuzzlePannel : SC_PuzzleFather
{
    
    
    [SerializeField] private SC_CursorPuzzle cursorPuzzle;

    [SerializeField] private UnityEvent onPuzzleCompleted;
    [SerializeField] private UnityEvent onPuzzleInteract;

    

    private bool CanSuccess;
    private bool canInteract;

    private void Awake()
    {
        CanSuccess = false;
        canInteract = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!CanSuccess)
        {
            //Quiero que solo si le he dado al boton de interactuar dentro de esto pueda hacerlo
            if (canInteract)
            {
                // SC_GameManager.Instance.OpenUI();
                //OpenUI();
                //
                onPuzzleInteract?.Invoke();
                Debug.Log("OnInteractionStartedHola");

                //no solo hay que ligarlo al puzle, debes abrir la UI del game manager
                //ShowPuzzlePannel?.Invoke(); en el cursor pannel
                canInteract = false;

            }

            else
            {
                // SC_GameManager.Instance.CloseUI();
                //CloseUI();
                //ReactivateUIButton?.Invoke(); ESTA EN EL CURSOR PUZLE

                onPuzzleCompleted?.Invoke();
                //tambien debes cerrar la ui y reactivar el botón de sta
            }
        }
    }

    private void OnEnable()
    {
        //cursorPuzzle.AddSuccess += CursorSendSuccess;
        SC_PlayerBrain.OnInteract += OnInteract;
    }

    // private void CursorPuzzleOnAddSuccess()
    // {
    //     CanSuccess++;
    // }

    private void OnDisable()
    {
        //cursorPuzzle.AddSuccess -= CursorSendSuccess;
        SC_PlayerBrain.OnInteract -= OnInteract;
    }

    public void CursorSendSuccess()
    {
        CanSuccess = true;
    }

    private void OnInteract()
    {
        Debug.Log("Interact");
        canInteract = true;
    }

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