using System;
using UnityEngine;

public class PuzzleLightCounter : MonoBehaviour
{
    public static PuzzleLightCounter Instance;

    public static System.Action OpenFinalDoor;
    
    private int lightCounter;

    public int puzzleCounterA;
    public int puzzleCounterB;
    public int puzzleCounterC;
    public int puzzleCounterD;
    
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
        if (lightCounter == 4)
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
        if (lightCounter == 4)
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
        if (lightCounter == 4)
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
        if (lightCounter == 4)
        {
            puzzleCounterD = 0;
            OpenFinalDoor?.Invoke();
        }
    }
    
    
    
}
