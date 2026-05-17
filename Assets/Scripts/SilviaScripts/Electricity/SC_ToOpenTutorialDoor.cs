using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_ToOpenTutorialDoor : MonoBehaviour
{
    [SerializeField] private GameObject doorA;
    [SerializeField] private GameObject doorB;
    
    [SerializeField] private float doorFinalMovement;
    
   
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
            doorA.transform.position += Vector3.back * Time.deltaTime;
            doorB.transform.position += Vector3.forward  * Time.deltaTime;
            yield return null;
        }
    }
    
    private void OnTriggerEnter(Collider other)//cuando el jugador entra por la puerta va al main juego
    {
        SceneManager.LoadScene("SampleScene");
    }
}
