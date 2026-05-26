using System;
using SilviaScripts.Electricity;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleLightCounter : MonoBehaviour
{
    public static PuzzleLightCounter Instance;

    public static System.Action OpenFinalDoor;
    
    public static Action OnPuzleAComplete;
    public int lightCounter {get; private set;}

    public int puzzleCounterA;
    public int puzzleCounterB;
    public int puzzleCounterC;
    public int puzzleCounterD;

    public int puzzleTutorial;

    public event Action<E_PuzzleType> OnBeginSuccessA;
    public event Action<E_PuzzleType> OnBeginSuccessB;
    public event Action<E_PuzzleType> OnBeginSuccessC;
    public event Action<E_PuzzleType> OnBeginSuccessD;
    
    void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

    public void SendBeginSuccess()
    {
        print("Reviso y lo mando");
        PuzzleCounterAComplete(E_PuzzleType.A);
        PuzzleCounterBComplete(E_PuzzleType.B);
        PuzzleCounterCComplete(E_PuzzleType.C);
        PuzzleCounterDComplete(E_PuzzleType.D);
    }

    private void PuzzleCounterDComplete(E_PuzzleType otherType)
    {
        if (puzzleCounterD > 0)
        {
            OnBeginSuccessD?.Invoke(E_PuzzleType.D);
            print("Es mayor que cero el puzle D");
        }
    }

    private void PuzzleCounterCComplete(E_PuzzleType otherType)
    {
        if (puzzleCounterC>0)OnBeginSuccessC?.Invoke(E_PuzzleType.C);
    }

    private void PuzzleCounterBComplete(E_PuzzleType otherType)
    {
        if (puzzleCounterB>0)OnBeginSuccessB?.Invoke(E_PuzzleType.B);
    }

    private void PuzzleCounterAComplete(E_PuzzleType otherType)
    {
        if (puzzleCounterA>0)OnBeginSuccessA?.Invoke(E_PuzzleType.A);
    }

    public void PuzzleAComplete()
    {
        if (puzzleCounterA > 0) return;
        
        lightCounter++;
        puzzleCounterA++;
        //OnPuzleAComplete?.Invoke();
        if (lightCounter == 4)//cambiar los valores d 4 a 5
        {
            //puzzleCounterA = 0;
            OpenFinalDoor?.Invoke();
        }
    }
    public void PuzzleBComplete()
    {
        if (puzzleCounterB > 0) return;
        lightCounter++;
        puzzleCounterB++;
        if (lightCounter == 4)
        {
            //puzzleCounterB = 0;
            OpenFinalDoor?.Invoke();
        }
    }
    public void PuzzleCComplete()
    {
        if (puzzleCounterC > 0) return;
        puzzleCounterC++;
        lightCounter++;
        if (lightCounter == 4)
        {
           // puzzleCounterC = 0;
            OpenFinalDoor?.Invoke();
        }
    }
    public void PuzzleDComplete()
    {
        if (puzzleCounterD > 0) return;
        puzzleCounterD++;
        lightCounter++;
        if (lightCounter == 4)
        {
            //puzzleCounterD = 0;
            OpenFinalDoor?.Invoke();
        }
    }

    public void PuzzleTutorialComplete(E_PuzzleType otherType)
    {
        if (puzzleTutorial > 0) return;
        puzzleTutorial++;
        lightCounter++;
        if (lightCounter == 4)
        {
            puzzleCounterD = 0;
            OpenFinalDoor?.Invoke();
        }
    }
    
    
    
    
}
