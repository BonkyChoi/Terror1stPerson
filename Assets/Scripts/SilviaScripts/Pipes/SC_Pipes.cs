using System;
using System.Collections;
using UnityEngine;

namespace SilviaScripts.Pipes
{
    public class SC_Pipes:MonoBehaviour
    {
        //como este sc s usa en  todas las instancias de cañeria no ncesita tener eventos
        [SerializeField] private float seconds = 180f;
        private static Vector3 playerLastPosition;
        private Coroutine resetCoroutine;


        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
               
                Debug.Log("detecto al player");
                playerLastPosition = other.transform.position;
                if (resetCoroutine != null) StopCoroutine(resetCoroutine);
                resetCoroutine = StartCoroutine(WaitUntilTimeFinish());
               
            }
        }

        private void OnTriggerStay(Collider other)
        {
           
            if (other.TryGetComponent<SC_PerceptionSystem>(out var perceptionSystem))
            {
                Debug.Log(perceptionSystem);
                perceptionSystem.NewPlayerLocation(playerLastPosition);
            }
        }

        private IEnumerator WaitUntilTimeFinish()
        {
            yield return new WaitForSeconds(seconds);
            playerLastPosition = Vector3.zero;

        }
    }
}