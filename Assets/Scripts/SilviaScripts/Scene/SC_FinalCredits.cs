using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_FinalCredits : MonoBehaviour
{
   [SerializeField] private RectTransform credits;
   private bool canCredits;
   [SerializeField]private float velocity;

   private void Start()
   {
      canCredits = true;
      StartCoroutine(WaitForCredits());
   }

   private IEnumerator WaitForCredits()
   {
      yield return new WaitForSeconds(30f);
      SceneManager.LoadScene("Titulo");
   }

   private void Update()
   {
      if(!canCredits)return;
      credits.anchoredPosition += Vector2.up * velocity * Time.deltaTime;

   }
}
