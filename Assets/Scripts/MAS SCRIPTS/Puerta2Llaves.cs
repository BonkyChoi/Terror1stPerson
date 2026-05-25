using UnityEngine;

public class Puerta2Llaves : MonoBehaviour
{
    public Animator anim;

    [Header("Llaves necesarias")]
    public Llave llave1;
    public Llave llave2;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip openSound;

    [Header("UI")]
    public GameObject mensajeUI;

    private bool enAnimacion = false;
    private bool puertaAbierta = false;

    private void Start()
    {
        if (mensajeUI != null)
            mensajeUI.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (enAnimacion || puertaAbierta) return;

        if (!other.CompareTag("Player")) return;

        Llave[] llavesJugador = other.GetComponentsInChildren<Llave>();

        bool tiene1 = false;
        bool tiene2 = false;

        foreach (Llave l in llavesJugador)
        {
            if (l == llave1) tiene1 = true;
            if (l == llave2) tiene2 = true;
        }

        if (tiene1 && tiene2)
        {
            AbrirPuerta(llavesJugador);
        }
        else
        {
            MostrarMensaje();
        }
    }
    private void AbrirPuerta(Llave[] llavesJugador)
    {
        enAnimacion = true;
        puertaAbierta = true;

        anim.SetBool("PuertaActiv", true);

        if (audioSource && openSound)
            audioSource.PlayOneShot(openSound);

        foreach (Llave l in llavesJugador)
        {
            if (l == llave1 || l == llave2)
            {
                Destroy(l.gameObject);
            }
        }

        if (mensajeUI != null)
            mensajeUI.SetActive(false);
    }
    private void MostrarMensaje()
    {
        if (mensajeUI == null) return;

        mensajeUI.SetActive(true);
        Invoke(nameof(OcultarMensaje), 3f);
    }
    private void OcultarMensaje()
    {
        if (mensajeUI != null)
            mensajeUI.SetActive(false);
    }
    public void FinAnimacion()
    {
        enAnimacion = false;
    }
}