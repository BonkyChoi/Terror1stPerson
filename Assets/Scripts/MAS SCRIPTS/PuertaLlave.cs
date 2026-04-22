using UnityEngine;

public class PuertaLlave : MonoBehaviour
{
    public Animator anim;
    public Llave llaveCorrecta;

    private bool enAnimacion = false;

    private void OnTriggerEnter(Collider other)
    {
        if (enAnimacion) return;

        if (other.CompareTag("Player"))
        {
            Llave llaveJugador = other.GetComponentInChildren<Llave>();

            if (llaveJugador == llaveCorrecta)
            {
                AbrirPuerta(llaveJugador);
            }
        }
    }
    private void AbrirPuerta(Llave llave)
    {
        enAnimacion = true;

        anim.SetBool("PuertaActiv", true);

        Destroy(llave.gameObject);
    }
    public void FinAnimacion()
    {
        enAnimacion = false;
    }
}