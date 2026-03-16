using System;
using UnityEngine;

public class SC_CursorPuzzle : MonoBehaviour
{
    public static System.Action Substract15Seconds;
    public static System.Action AddSuccess;

    private void OnEnable()
    {
        SC_PuzzlePannel.ShowPuzzlePannel += ShowPuzzlePannel;
    }
    private void OnDisable()
    {
        SC_PuzzlePannel.ShowPuzzlePannel -= ShowPuzzlePannel;
    }

    private void ShowPuzzlePannel()
    {
        //programar bien el puzle
        //permitir clicar
        //si acierta -> current success ++
        AddSuccess?.Invoke();
        //currentTimesToSuccess++;
        //si falla -> substract invoke
        Substract15Seconds?.Invoke();
        Debug.Log("Substract15Seconds");
    }

    

}
