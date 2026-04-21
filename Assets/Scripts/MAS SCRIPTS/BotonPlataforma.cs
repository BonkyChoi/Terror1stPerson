using UnityEngine;

public class BotonPlataforma : MonoBehaviour
{
    public PlataformaMovil plataforma;
    private bool playerInside;

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Botón activado");
            plataforma.Activar();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            Debug.Log("Player dentro del botón");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            Debug.Log("Player fuera del botón");
        }
    }
}