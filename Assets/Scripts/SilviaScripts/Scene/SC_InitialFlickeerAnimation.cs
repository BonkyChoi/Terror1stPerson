using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class SC_InitialFlickeerAnimation : MonoBehaviour
{
    [SerializeField]private Image hudImage;
    //private PlayerMovementV playerMovement;
    private int timesDeath = 0;

    private void Awake()
    {
        //playerMovement = GetComponent<PlayerMovementV>();
        //playerMovement.enabled = true;
    }

    private void Start()
    {
        if (SC_DeathCounter.Instance.DeathCounter == 0)
        {
            hudImage.enabled = false;
            
            timesDeath++;
        }
        else
        {
            BeginToFlick();
        }
       
        
    }
    

    private void BeginToFlick()
    {
        Debug.Log("Voy a flickear");
        
        //playerMovement.enabled = false;
        StartCoroutine(BeginToFlickCoroutine());
    }

    
    

    private IEnumerator BeginToFlickCoroutine()
    {
       float randomFloat = Random.value;
       float secondsForWhite = Random.Range(0.05f, 0.1f);
       float secondsForTransparent = Random.Range(0.05f,0.1f);
        Image newImage = hudImage.GetComponent<Image>();
        Color newColor = newImage.color;
        timesDeath++;
        hudImage.enabled = true;
        hudImage.color = Color.white;
        newColor.a = 1;
        yield return new WaitForSeconds(secondsForTransparent);
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
       // playerMovement.enabled = true;
        
        
    }
    // Para el movimiento del jugador y su interaccion
    // Realiza un flicker con una animacion en el ca
    
    //randomiza un numero del 0 al 1
    //le asigna ese numero a la opacidad
    //repite 4 veces
    //lo deja en opacidad 0 
    
    
    
    // Permite el movimiento del jugador
    
}
