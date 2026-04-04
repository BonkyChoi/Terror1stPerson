using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class SC_HandWrittingManager : MonoBehaviour
{
    [SerializeField] private string[] texts;
    private Coroutine typeWriterCoroutine;
    private int currentVisibleCharacterIndex;
    private string textToShow;
    [SerializeField]private TextMeshProUGUI textBox;
    
    
    [Header("Typewriter Settings")]
    [Tooltip("20")][SerializeField] private float firstCharacterPerSecond; //cuantas letras x seg van a aparecer
    [Tooltip("0.5")][SerializeField] private float firstInterpunctuationDelay; //delay en signos de puntuacion
    private float delay;
    


    public static Action OnTextWritted;
    public static Action CharacterRevealer;

    private void Awake()
    {
        
    }
   private void Start()
    {
        //Randomiza el mensaje que sale 
        if (texts.Length<=0)return;
        textToShow = texts[Random.Range(0, texts.Length)];
        StartTyping(textToShow);
        
        //el que salga lo escribe y cuando termina avisa de que ya se puede ir poniendo en blanco la pantalla
    }
   
    private void StartTyping(string textToShow)
    {
        if (typeWriterCoroutine != null)
            StopCoroutine(typeWriterCoroutine);

        

        textBox.text = textToShow;
        textBox.ForceMeshUpdate();
        textBox.maxVisibleCharacters = 0;
        currentVisibleCharacterIndex = 0;

        typeWriterCoroutine = StartCoroutine(TypeWriter());
    }

    private IEnumerator TypeWriter()
    {
        textBox.ForceMeshUpdate();
        TMP_TextInfo textInfo = textBox.textInfo;//recogemos la info del la textBox

        while (currentVisibleCharacterIndex < textInfo.characterCount) //mas uno pq siempre te cuenta el nnumero 0
        {
           
            char character = textInfo.characterInfo[currentVisibleCharacterIndex].character; //le damos al caracter la informacion del caracter que este visible en ese momento
            textBox.maxVisibleCharacters++;//vamos aumentando los caractetres en pantalla
            CharacterRevealer?.Invoke();

            

            

            bool isPunctuation = character == '?' || character == '¿' || character == '¡' ||
                                 character == '!' || character == ',' || character == ';' ||
                                 character == '.' || character == ':' || character == '-';//si es un signo d puntuacion hacemos q lo espere sino no
            
            
            if (isPunctuation)
            {
                delay = firstInterpunctuationDelay;
            }
            else
            {
                delay = 1f / firstCharacterPerSecond;
            }

            yield return new WaitForSeconds(delay);
            currentVisibleCharacterIndex++;
        }

        yield return new WaitForSeconds(1f);
        OnTextWritted?.Invoke();
        
        while (textBox.maxVisibleCharacters > 0) //mas uno pq siempre te cuenta el nnumero 0
        {
            textBox.maxVisibleCharacters--;//vamos aumentando los caractetres en pantalla
            yield return new WaitForSeconds(0.03f);
        }
        
    
    }
    // Escribe el mensaje del array 
    // Array randomizado
    // Manda un mensaje cuando se haya terminado y permite que empiece la pantalla a ponerse en blanco
}
