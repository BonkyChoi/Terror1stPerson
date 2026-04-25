using System;
using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

public class SC_ListeningSystem : MonoBehaviour
{
    // Cuando detecta un sonido mira al lugar de donde ha surgido
    //Lo de las tuberías se hará con waypoints en el lugar que hagan de zona donde finalice la tubería. Estas tuberías iran con eventos y
    //cuando detecten un sonido mandarán a todas las subscritas si escuchan al player, de escucharlo, le darán la localización al enemigo

    [SerializeField] private SphereCollider monsterEars;//debe determinar el radio de escuha del monstruo
    private NavMeshAgent agent;
    private bool haveATarget;
    private bool listeningCoroutine;

    public static Action<Vector3> TargetPosition;
    public static Action CanSeeTarget;
    public static Action CantSeeTarget;
    
    

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("DetectorEnemy"))
        {
            haveATarget = true;
            if (listeningCoroutine) return;
            listeningCoroutine = true;
            CanSeeTarget?.Invoke();
            StartCoroutine(InvokeHaveATarget(other.transform));
        }
    }

    private IEnumerator InvokeHaveATarget(Transform otherTransform)
    {
        while (haveATarget)
        {
            TargetPosition?.Invoke(otherTransform.position);
            yield return new WaitForSeconds(5);
        }
    }

   
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DetectorEnemy"))
        {
            haveATarget = false;
            StopAllCoroutines();
            listeningCoroutine = false;
            CantSeeTarget?.Invoke();
        }
    }

  
}
