using System;
using System.Collections;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class SC_CursorPuzzle : MonoBehaviour
{
    public static System.Action Substract15Seconds;
    public static System.Action AddSuccess;
    

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
    [SerializeField] private PlayerInput playerInput; //a


    private void Start()
    {
        //playerInput.actions["PauseCursor"].Disable();
        puzzlePanel.SetActive(false);
        
        tutorialPanel.SetActive(false);
        beginPanel.SetActive(false);
        exitPanel.SetActive(false);
    }

    private void OnEnable()
    {
        SC_PuzzlePannel.ShowPuzzlePannel += ShowPuzzlePannel;
        playerInput.actions["BeginMiniGame"].started += Onstarted;
        //playerInput.actions["PauseCursor"].started += OnPauseCursor;
        //SC_UIBrain.OnMiniGame += OnMiniGame;
        SC_UIBrain.OnExitGame += OnExitGame;
        SC_PuzzlePannel.ReactivateUIButton += ReactivateUIButton;
    }

    private void ReactivateUIButton()
    {
        OnExitGame();
    }

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
            AddSuccess?.Invoke();
        }
        else
        {
            Debug.Log("Has fallado");
            Substract15Seconds?.Invoke();
        }
        //si el cursor esta entre zona inicio objetivo [la rotación en z] y zona final objetivo [que tanto fill amount tiene] es success:es fail;
        yield return new WaitForSeconds(0.7f);
        BeginPlay();
    }

    private void Onstarted(InputAction.CallbackContext obj)
    {
        Debug.LogWarning("MiniGame");
        tutorialPanel.SetActive(false);
        beginPanel.SetActive(false);
        BeginPlay();
    }

    private void OnExitGame()
    {
        playerInput.actions["BeginMiniGame"].Enable();
        HUD.SetActive(true);
        puzzlePanel.SetActive(false);
    }

    private void OnDisable()
    {
        SC_PuzzlePannel.ShowPuzzlePannel -= ShowPuzzlePannel;
        
    }
    


    private void ShowPuzzlePannel()
    {
        Debug.Log("ShowPuzzlePannel");//no aparece
        HUD.SetActive(false);
        puzzlePanel.SetActive(true);
         
        if (PuzzleLightCounter.Instance.lightCounter == 0) tutorialPanel.SetActive(true);
        else beginPanel.SetActive(true);
        //cuando pulse una tecla que se le permitira pulsar comienza el juego
        //programar bien el puzle
        
        
        //permitir clicar
        //si acierta -> current success ++
       // AddSuccess?.Invoke();
        //currentTimesToSuccess++;
        //si falla -> substract invoke
       // Substract15Seconds?.Invoke();
        Debug.Log("Substract15Seconds");
    }

    private void BeginPlay()
    {
        playerInput.actions["BeginMiniGame"].Disable();
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
        playerInput.actions["PauseCursor"].started += OnPauseCursor;
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
