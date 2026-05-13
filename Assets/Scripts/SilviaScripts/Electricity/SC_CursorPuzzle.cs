using System;
using System.Collections;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class SC_CursorPuzzle : MonoBehaviour
{
    public static System.Action Substract15Seconds;
    //public event System.Action AddSuccess;//le añade el success cuando ya lo ha logrado

    private int successTimes;
    
    //con problemas para ahora hacer varios, se va a pasar a eventos asi que nos quitamos las acciones estaticas
    

    [SerializeField] private GameObject HUD; 
    [SerializeField] private GameObject puzzlePanel;
    [SerializeField] private Transform cursor;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject beginPanel;
    [SerializeField] private GameObject exitPanel;
    [SerializeField] private Image zone;
    [SerializeField] private RectTransform zoneRect;
    private bool cursorCanMove;
    [SerializeField]private float speed;
    private bool beginPlay;
    //[SerializeField] private PlayerInput playerInput; //a
    
    
    //Desde este script se crea delegado y en el start de este mismo script por el game player controller
    //al delegado de este script bindear la funcion de desactivar o activar del player controller

    // delegate void Delegate();
    // private Delegate enableMiniGame;
    // private Delegate disableMiniGame;
    private bool resolving;
    private bool puzzleActive;
    private bool gameStarted;

    private int totalTimesToSuccess = 3;
    
    [SerializeField] private UnityEvent OnSuccess;

    private void Start()
    {
        // enableMiniGame = SC_GameUtilitys.GetPlayerController().EnableBeginMiniGame;
        // enableMiniGame.Invoke();
        // disableMiniGame = SC_GameUtilitys.GetPlayerController().DisableBeginMiniGame;
        puzzlePanel.SetActive(false);
        tutorialPanel.SetActive(false);
        beginPanel.SetActive(false);
        exitPanel.SetActive(false);
        successTimes = 0;
    }
    private void OnEnable()
    {
        SC_InputEvent.OnBeginMiniGame += StartMiniGame;
        SC_InputEvent.OnPauseCursor += TryStopCursor;
        //SC_PuzzlePannel.ShowPuzzlePannel += ShowPuzzlePannel;
        //SC_PuzzlePannel.ReactivateUIButton += ReactivateUIButton;
        SC_UIBrain.OnExitGame += OnExitGame;
        
    }

    private void StartMiniGame()
    {
        if (!puzzleActive) return;

        if (gameStarted) return;

        gameStarted = true;

        tutorialPanel.SetActive(false);
        beginPanel.SetActive(false);

        BeginPlay();
    }

    private void OnDisable()
    {
      
       SC_InputEvent.OnBeginMiniGame -= StartMiniGame;
       SC_InputEvent.OnPauseCursor -= TryStopCursor;

       //SC_PuzzlePannel.ShowPuzzlePannel -= ShowPuzzlePannel;
       //SC_PuzzlePannel.ReactivateUIButton -= ReactivateUIButton;
       SC_UIBrain.OnExitGame -= OnExitGame;
    }

    private void TryStopCursor()
    {
        if (!puzzleActive) return;

        if (!gameStarted) return;

        if (!cursorCanMove) return;

        if (resolving) return;

        resolving = true;

        cursorCanMove = false;

        StartCoroutine(MouseDownCoroutine());
    }

    

    // public void ReactivateUIButton()
    // {
    //     OnExitGame();
    // }

    private void OnPauseCursor(InputAction.CallbackContext obj)
    {
        cursorCanMove = false;
        StartCoroutine(MouseDownCoroutine());
    }

    private IEnumerator MouseDownCoroutine()
    {
        
        float angle = cursor.transform.eulerAngles.z;
        float zoneStart = zoneRect.eulerAngles.z;
        float zoneSize = zone.fillAmount * 360f;
        
        // Diferencia angular segura (-180 a 180)
        float delta = (angle - zoneStart + 360f) % 360f;
        
        
        if (delta <= zoneSize)
        {
            Debug.Log("Ha funcionado");
            //throw (new ArgumentException("todo bien"));
            successTimes++;
            if (successTimes >= totalTimesToSuccess)
            {
                OnExitGame();
                OnSuccess?.Invoke();
            }
        }
        else
        {
            Debug.Log("Has fallado");
            Substract15Seconds?.Invoke();
        }
        //si el cursor esta entre zona inicio objetivo [la rotación en z] y zona final objetivo [que tanto fill amount tiene] es success:es fail;
        yield return new WaitForSeconds(0.7f);
        resolving = false;
        if (!puzzleActive) yield break;
        BeginPlay();
    }

    // private void AddSuccess()
    // {
    //     successTimes++;
    // }

    // private void Onstarted(InputAction.CallbackContext obj)
    // {
    //     Debug.LogWarning("MiniGame");
    //     tutorialPanel.SetActive(false);
    //     beginPanel.SetActive(false);
    //     BeginPlay();
    // }

    public void OnExitGame() //quitas la UI
    {
        Debug.Log("OnExitGame");
        //playerInput.actions["BeginMiniGame"].Enable();
        //enableMiniGame.Invoke();
        puzzleActive = false;
        gameStarted = false;
        cursorCanMove = false;

        HUD.SetActive(true);
        puzzlePanel.SetActive(false);
    }
    
    
    public void ShowPuzzlePannel()//le llamas con los unity events
    {
        puzzleActive = true;

        HUD.SetActive(false);
        puzzlePanel.SetActive(true);

        if (PuzzleLightCounter.Instance.lightCounter == 0)
            tutorialPanel.SetActive(true);
        else
            beginPanel.SetActive(true);
        //cuando pulse una tecla que se le permitira pulsar comienza el juego
        //programar bien el puzle
        
        
        //permitir clicar
        //si acierta -> current success ++
       // AddSuccess?.Invoke();
        //currentTimesToSuccess++;
        //si falla -> substract invoke
       // Substract15Seconds?.Invoke();
    }
    
    private void BeginPlay()
    {
        //playerInput.actions["BeginMiniGame"].Disable();
        //disableMiniGame.Invoke();
        exitPanel.SetActive(true);

        float safeZone = Random.value;
        zone.fillAmount = safeZone;
        //calcula zona entre 0 y 1
        float newRotation = Random.rotation.eulerAngles.z;
        zoneRect.rotation = Quaternion.Euler(0, 0, newRotation);
        //calcula su rotacion
        CursorBeginMovement();

    }

    private void CursorBeginMovement()
    {
        //playerInput.actions["PauseCursor"].Enable();
        //playerInput.actions["PauseCursor"].started += OnPauseCursor;
        
        cursorCanMove = true;
        
    }


    private void Update()
    {
        if (!cursorCanMove)return;
        CursorMovement();
    }

    private void CursorMovement()
    {
        //el cursor da vueltas a cierta velocidad
        cursor.Rotate(0,0, -1 * speed * Time.deltaTime);
        //cursor.rotation = Quaternion.Euler(0, 0, speed * Time.deltaTime);
    }

    

    

}
