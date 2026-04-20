using System;
using UnityEngine;

public class SC_PuzzlePannel : MonoBehaviour
{
    //Este solo es lo que abre el canvas, no tiene mas lgica

    public static System.Action ShowPuzzlePannel;
    
    [SerializeField] private int totalTimesToSuccess;
    private int currentTimesToSuccess;

    private void OnTriggerStay(Collider other)
    {
        //te permite abrirlo mientras que hayas acertado menos de las veces totales
        if (other.CompareTag("Player"))
            if (currentTimesToSuccess < totalTimesToSuccess) // esto en nuevo sistema de Inputs// Input.GetKey(KeyCode.E))
            {
                ShowPuzzlePannel?.Invoke();
            }
            else
            {
                //segun el tipo de puzle (se vera en herencia) le dice al correspondiente PuzzleLightCounter que se encienda
            }
    }

    private void OnEnable()
    {
        SC_CursorPuzzle.AddSuccess += AddSuccess; //recibe que has acertado y lo añade al contador
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
