using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SC_ChangeIntensity : MonoBehaviour
{
    private bool canUpdateColor;
    [SerializeField] private Image image;
    public static System.Action<int> BeginToFlick;

    // Cambia progresivamente el valor del color a blanco
    // Una vez que es blanco espera 3 segundos
    // Te manda a la escena del juego
    private void OnEnable()
    {
        SC_HandWrittingManager.OnTextWritted += OnTextWritted;
    }

    private void OnTextWritted()
    {
        canUpdateColor = true;
        
    }

    private void Update()
    {
        if (!canUpdateColor) return;
        CanUpdateImageColor();
    }

    private void CanUpdateImageColor()
    {
        Image newImage = image.GetComponent<Image>();
        Color newImageColor = newImage.color;
        newImageColor.b += Time.deltaTime;
        newImage.color = newImageColor;
        newImageColor.r += Time.deltaTime;
        newImage.color = newImageColor;
        newImageColor.g += Time.deltaTime;
        newImage.color = newImageColor;

        if (newImage.color.r >= 1 && newImage.color.g >= 1 && newImage.color.b >= 1)
        {
            image.color = Color.white;
            canUpdateColor = false;
            StartCoroutine(BeginToTurnWhite());
        }
        
    }

    private IEnumerator BeginToTurnWhite()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("PruebasElectricidad");//Esto es provisional, te debe mandar al centro de la sala que es de donde sales en un primer momeento
        
        //El progreso que hayas logrado se debe guardar en una instancia que no se destruya para no volver a repetir el juego
    }
}
