using UnityEngine;

public class PuertaGeneral : MonoBehaviour
{
    public Animator anim;

    private bool enZona;
    private bool activa;
    private bool enAnimacion = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && enZona && !enAnimacion)
        {
            enAnimacion = true;

            activa = !activa;
            anim.SetBool("PuertaActiv", activa);
        }
    }
    public void FinAnimacion()
    {
        enAnimacion = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enZona = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enZona = false;
        }
    }
}