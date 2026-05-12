using System;
using System.Collections;
using UnityEngine;

namespace SilviaScripts.Pipes
{
    public class SC_Pipes:MonoBehaviour
    {
        //como este sc s usa en  todas las instancias de cañeria no ncesita tener eventos
        [SerializeField] private float seconds = 180f;
 HEAD
        public static Vector3 playerLastPosition;
        private Coroutine resetCoroutine;

        //public static Vector3 playerLastPosition;
        //public static float lastTimeHeard;
        public static Action<Vector3> OnPlayerPosition;

 7a044f5 (ur ur)


        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
               
                Debug.Log("detecto al player");
 HEAD
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
                //playerLastPosition = other.transform.position;
                Debug.Log(other.transform.position);
                OnPlayerPosition?.Invoke(other.transform.position);
                //lastTimeHeard = Time.time;
                
                
                
            }
        }

       //rivate void OnTriggerStay(Collider other)
       //
       //  
       //   if (other.TryGetComponent<SC_PerceptionSystem>(out var perceptionSystem))
       //   {
       //       Debug.Log(perceptionSystem);
       //       perceptionSystem.NewPlayerLocation(playerLastPosition);
       //   }
       //

        // private IEnumerator WaitUntilTimeFinish()
        // {
        //     yield return new WaitForSeconds(seconds);
        //     playerLastPosition = Vector3.zero;
        //
        // }
        
        //public static bool HasValidPosition(float maxTime)
        //{
          //  return Time.time - lastTimeHeard <= maxTime;
        //}
7a044f5 (ur ur)
    }
}