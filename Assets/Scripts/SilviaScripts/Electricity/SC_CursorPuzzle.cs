using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class SC_CursorPuzzle : MonoBehaviour
{
    public static System.Action Substract15Seconds;
    public static System.Action AddSuccess;

    [SerializeField] private GameObject HUD; 
    [SerializeField] private GameObject puzzlePanel;
    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject beginPanel;
    [SerializeField] private GameObject exitPanel;
    [SerializeField] private Image zone;
    [SerializeField] private RectTransform zoneRect;
    private bool cursorCanMove;
    [SerializeField]private float speed;



    private void Start()
    {
        puzzlePanel.SetActive(false);
        cursor.SetActive(false);
        tutorialPanel.SetActive(false);
        beginPanel.SetActive(false);
        exitPanel.SetActive(false);
    }

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
        HUD.SetActive(false);
        puzzlePanel.SetActive(true);
        cursor.SetActive(true);
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
        tutorialPanel.SetActive(false);
        beginPanel.SetActive(false);
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
        cursorCanMove = true;
        
    }

    private void OnMouseDown()
    {
        cursorCanMove = false;
        float angle = cursor.transform.eulerAngles.z;
        if (angle >= zoneRect.rotation.eulerAngles.z && angle <= zoneRect.rotation.eulerAngles.z + zone.fillAmount *360f) AddSuccess?.Invoke();
        else Substract15Seconds?.Invoke();
        //si el cursor esta entre zona inicio objetivo [la rotación en z] y zona final objetivo [que tanto fill amount tiene] es success:es fail;
        
        BeginPlay();
    }

    private void Update()
    {
        if (!cursorCanMove)return;
        CursorMovement();
    }

    private void CursorMovement()
    {
        //el cursor da vueltas a cierta velocidad
        cursor.transform.Rotate(0,0, speed * Time.deltaTime);
    }

    private void UnShowPuzzlePannel()
    {
        //se debería de poder salir de la pantalla del puzle si se deseara [Boton de exit]
        HUD.SetActive(true);
        puzzlePanel.SetActive(false);
    }

    

}
