using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class SC_InitialFlickeerAnimation : MonoBehaviour
{
    private Image hudImage;
    private PlayerMovementV playerMovement;

    private void Awake()
    {
        hudImage = GetComponent<Image>();
        playerMovement = GetComponent<PlayerMovementV>();
    }

    private void Start()
    {
        playerMovement.enabled = false;
        hudImage.color = Color.white;
        
    }

    private void OnEnable()
    {
        SC_ChangeIntensity.BeginToFlick += BeginToFlick;
    }

    private void OnDisable()
    {
        SC_ChangeIntensity.BeginToFlick -= BeginToFlick;
    }

    private void BeginToFlick()
    {
        StartCoroutine(BeginToFlickCoroutine());
    }

    private IEnumerator BeginToFlickCoroutine()
    {
       float randomFloat = Random.value;
       float secondsForWhite = Random.value;
       float secondsForTransparent = Random.value;
        Image newImage = hudImage.GetComponent<Image>();
        Color newColor = newImage.color;
        for (int i = 0; i < 4; i++)
        {
            newColor.a = randomFloat;
            newImage.color = newColor;
            yield return new WaitForSeconds(secondsForWhite);
            newColor.a = 1;
            newImage.color = newColor;
            yield return new WaitForSeconds(secondsForTransparent);
        }
        newColor.a = 0;
        newImage.color = newColor;
        playerMovement.enabled = true;
        
        
    }
    // Para el movimiento del jugador y su interaccion
    // Realiza un flicker con una animacion en el ca
    
    //randomiza un numero del 0 al 1
    //le asigna ese numero a la opacidad
    //repite 4 veces
    //lo deja en opacidad 0 
    
    
    
    // Permite el movimiento del jugador
    
}
