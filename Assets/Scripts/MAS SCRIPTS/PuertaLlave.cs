using UnityEngine;
using System.Collections;

public class PuertaLlave : MonoBehaviour
{
    public Animator anim;
    public Llave llaveCorrecta;

    [Header("UI")]
    public GameObject mensajeUI;

    private bool enAnimacion = false;
    private Coroutine mensajeCoroutine;

    private void Start()
    {
        if (mensajeUI != null)
            mensajeUI.SetActive(false);
    }
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
            else
            {
                MostrarMensaje();
            }
        }
    }
    private void AbrirPuerta(Llave llave)
    {
        enAnimacion = true;

        anim.SetBool("PuertaActiv", true);

        Destroy(llave.gameObject);
    }
    private void MostrarMensaje()
    {
        if (mensajeUI == null) return;

        mensajeUI.SetActive(true);
        
        if (mensajeCoroutine != null)
            StopCoroutine(mensajeCoroutine);

        mensajeCoroutine = StartCoroutine(OcultarMensaje());
    }
    private IEnumerator OcultarMensaje()
    {
        yield return new WaitForSeconds(3f);

        mensajeUI.SetActive(false);
    }
    public void FinAnimacion()
    {
        enAnimacion = false;
    }
}