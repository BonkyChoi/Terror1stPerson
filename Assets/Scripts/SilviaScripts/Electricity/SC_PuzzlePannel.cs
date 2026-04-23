using System;
using UnityEngine;

public class SC_PuzzlePannel : MonoBehaviour
{
    //Este solo es lo que abre el canvas, no tiene mas lgica

    public static System.Action ShowPuzzlePannel;
    
    [SerializeField] private int totalTimesToSuccess;
    private int currentTimesToSuccess;
    private bool canInteract;

    private void OnTriggerStay(Collider other)
    {
        //te permite abrirlo mientras que hayas acertado menos de las veces totales
        if (TryGetComponent<PlayerMovementV>(out var playerMovement))
            if (currentTimesToSuccess < totalTimesToSuccess) // esto en nuevo sistema de Inputs// Input.GetKey(KeyCode.E))
            {
                //Quiero que solo si le he dado al boton de interactuar dentro de esto pueda hacerlo
                if (canInteract)
                {
                    playerMovement.enabled = false;
                    ShowPuzzlePannel?.Invoke();
                    SC_GameManager.Instance.OpenUI();
                    canInteract = false;
                    
                }
            }
            else
            { playerMovement.enabled = true; 
                //segun el tipo de puzle (se vera en herencia) le dice al correspondiente PuzzleLightCounter que se encienda
            }
    }

    private void OnEnable()
    {
        SC_CursorPuzzle.AddSuccess += AddSuccess; //recibe que has acertado y lo añade al contador
        SC_PlayerBrain.OnInteract += () => canInteract = true;
    }
    
    private void OnDisable()
    {
        SC_CursorPuzzle.AddSuccess -= AddSuccess;
        SC_PlayerBrain.OnInteract -= () => canInteract = true;
    }

    private void AddSuccess()
    {
        currentTimesToSuccess++;
    }
}
