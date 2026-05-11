using UnityEngine;

public class BotonPlataformaAutomatica : MonoBehaviour
{
    public PlataformaMovil plataforma;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            plataforma.Activar();
        }
    }
}
