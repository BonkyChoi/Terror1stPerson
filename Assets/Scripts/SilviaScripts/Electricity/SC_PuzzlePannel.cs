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

    [SerializeField] private int totalTimesToSuccess;

    private int currentTimesToSuccess;
    public bool CanInteract {get; private set;}

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (currentTimesToSuccess < totalTimesToSuccess)
        {
            if (CanInteract)
            {
                SC_GameManager.Instance.OpenUI();

                ShowPuzzlePannel?.Invoke();
                
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
        cursorPuzzle.AddSuccess += CursorPuzzleOnAddSuccess;
        SC_PlayerBrain.OnInteract += OnInteract;
    }

    private void CursorPuzzleOnAddSuccess()
    {
        currentTimesToSuccess++;
    }

    private void OnDisable()
    {
        cursorPuzzle.AddSuccess -= CursorPuzzleOnAddSuccess;
        SC_PlayerBrain.OnInteract -= OnInteract;
    }

    private void OnInteract()
    {
        Debug.Log("Interact");
        CanInteract = true;
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