using System;
using UnityEngine;

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
    
    public void PuzzleAComplete()
    {
        if (puzzleCounterA > 0) return;
        
        lightCounter++;
        puzzleCounterA++;
        //OnPuzleAComplete?.Invoke();
        if (lightCounter == 5)
        {
            puzzleCounterA = 0;
            OpenFinalDoor?.Invoke();
        }
    }
    public void PuzzleBComplete()
    {
        if (puzzleCounterB > 0) return;
        lightCounter++;
        puzzleCounterB++;
        if (lightCounter == 5)
        {
            puzzleCounterB = 0;
            OpenFinalDoor?.Invoke();
        }
    }
    public void PuzzleCComplete()
    {
        if (puzzleCounterC > 0) return;
        puzzleCounterC++;
        lightCounter++;
        if (lightCounter == 5)
        {
            puzzleCounterC = 0;
            OpenFinalDoor?.Invoke();
        }
    }
    public void PuzzleDComplete()
    {
        if (puzzleCounterD > 0) return;
        puzzleCounterD++;
        lightCounter++;
        if (lightCounter == 5)
        {
            puzzleCounterD = 0;
            OpenFinalDoor?.Invoke();
        }
    }

    public void PuzzleTutorialComplete()
    {
        if (puzzleTutorial > 0) return;
        puzzleTutorial++;
        lightCounter++;
        if (lightCounter == 5)
        {
            puzzleCounterD = 0;
            OpenFinalDoor?.Invoke();
        }
    }
    
    
    
    
}
