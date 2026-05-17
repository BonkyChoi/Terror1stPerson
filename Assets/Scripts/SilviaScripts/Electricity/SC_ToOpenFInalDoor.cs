using System;
using System.Collections;
using UnityEngine;

public class SC_ToOpenFInalDoor : MonoBehaviour
{
    [SerializeField] private GameObject doorA;
    [SerializeField] private GameObject doorB;
    
    [SerializeField] private float doorFinalMovement;
    
    public static System.Action ShowCredits;
    
    
    private Vector3 doorFInalAFirstPosition;
    private Vector3 doorFInalAFinalPosition;
    private Vector3 doorFInalBFirstPosition;
    private Vector3 doorFInalBFinalPosition;
    private bool isOpening;


    public void OpenFinalDoor()
    {
        if (isOpening) return;
        StartCoroutine(OpenFinalDoorCoroutine());
        isOpening = true;
    }

    private IEnumerator OpenFinalDoorCoroutine()
    {
        while (Vector3.Distance(doorA.transform.position, doorB.transform.position) < doorFinalMovement)
        {
            doorA.transform.position += Vector3.left * Time.deltaTime;
            doorB.transform.position += Vector3.right  * Time.deltaTime;
            yield return null;
        }
    }
    
    private void OnTriggerExit(Collider other)//cuando el jugador sale por la puerta termina el juego
    {
        if(other.CompareTag("Player"))
            ShowCredits?.Invoke();
        print("fin");
    }
}
