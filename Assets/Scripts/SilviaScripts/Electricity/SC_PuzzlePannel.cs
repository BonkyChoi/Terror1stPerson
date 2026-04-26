using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SC_PuzzlePannel : MonoBehaviour
{
    //Este solo es lo que abre el canvas, no tiene mas lgica

    public static System.Action ShowPuzzlePannel;
    public static System.Action ReactivateUIButton;
    
    [SerializeField] private int totalTimesToSuccess;
    private int currentTimesToSuccess;
    private bool canInteract;
   
   [SerializeField] private PlayerInput playerInput;

    private void OnTriggerStay(Collider other)
    {
        //te permite abrirlo mientras que hayas acertado menos de las veces totales
        if (other.CompareTag("Player"))
            if (currentTimesToSuccess < totalTimesToSuccess) // esto en nuevo sistema de Inputs// Input.GetKey(KeyCode.E))
            {
                //Quiero que solo si le he dado al boton de interactuar dentro de esto pueda hacerlo
                if (canInteract)
                {
                    //era esto, como no lo ha hecho en inputs al querer desactivar el movimiento desactivo el componente entero y ya no pilla el try get component
                    SC_GameManager.Instance.OpenUI();
                    
                    ShowPuzzlePannel?.Invoke(); 
                    //SC_GameManager.Instance.OpenUI();
                    SC_GameManager.Instance.OpenUI();
                    Debug.Log(playerInput.currentActionMap);
                    canInteract = false;
                    
                }
            }
            else
            { 
               SC_GameManager.Instance.CloseUI();
               ReactivateUIButton?.Invoke(); //puedes volver a pulsar ya que ya no esta el menu
               
               //segun el tipo de puzle (se vera en herencia) le dice al correspondiente PuzzleLightCounter que se encienda
            }
    }

    private void OnEnable()
    {
        SC_CursorPuzzle.AddSuccess += AddSuccess; //recibe que has acertado y lo añade al contador
        SC_PlayerBrain.OnInteract += OnInteract;
    }
    
    //


    private void OnInteract()
    {
        Debug.Log("Interact");
        canInteract = true;
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
