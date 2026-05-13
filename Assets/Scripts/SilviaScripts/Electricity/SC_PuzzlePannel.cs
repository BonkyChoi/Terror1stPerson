using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SC_PuzzlePannel : MonoBehaviour
{
    public static System.Action ShowPuzzlePannel;
    public static System.Action ReactivateUIButton;
    [SerializeField] private SC_CursorPuzzle cursorPuzzle;

    [SerializeField] private UnityEvent onPuzzleCompleted;

    

    private bool CanSuccess;
    private bool canInteract;

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (CanSuccess)
        {
            //Quiero que solo si le he dado al boton de interactuar dentro de esto pueda hacerlo
            if (canInteract)
            {
                SC_GameManager.Instance.OpenUI();

                ShowPuzzlePannel?.Invoke();
                canInteract = false;
                
            }
        }
        else
        {
            SC_GameManager.Instance.CloseUI();

            ReactivateUIButton?.Invoke();

            onPuzzleCompleted?.Invoke();
        }
    }

    private void OnEnable()
    {
        cursorPuzzle.AddSuccess += CursorSendSuccess;
        SC_PlayerBrain.OnInteract += OnInteract;
    }

    // private void CursorPuzzleOnAddSuccess()
    // {
    //     CanSuccess++;
    // }

    private void OnDisable()
    {
        cursorPuzzle.AddSuccess -= CursorSendSuccess;
        SC_PlayerBrain.OnInteract -= OnInteract;
    }

    private void CursorSendSuccess()
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