using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SC_EyeFlicker : MonoBehaviour
{
    private Image hudImage;

    private void Awake()
    {
        hudImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        SC_InitialFlickeerAnimation.OnBeginToFlick += BeginFlicker;
    }
    
    private void OnDisable()
    {
        SC_InitialFlickeerAnimation.OnBeginToFlick -= BeginFlicker;
    }

    private void BeginFlicker()
    {
        
        SC_GameManager.Instance.OpenUI();
        StartCoroutine(BeginToFlickCoroutine());
    }

    
    

    private IEnumerator BeginToFlickCoroutine()
    {
        float randomFloat = Random.value;
        float secondsForWhite = Random.Range(0.05f, 0.1f);
        float secondsForTransparent = Random.Range(0.05f,0.1f);
        Image newImage = hudImage.GetComponent<Image>();
        Color newColor = newImage.color;
        //timesDeath++;
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
        SC_GameManager.Instance.CloseUI();
        
        
    }
}
