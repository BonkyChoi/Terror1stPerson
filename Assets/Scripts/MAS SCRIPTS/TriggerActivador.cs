using UnityEngine;

public class TriggerActivador : MonoBehaviour
{
    public PlataformaPingPong plataforma;

    private void OnTriggerEnter(Collider other)
    {
        plataforma.ActivarMovimiento();
    }
}
