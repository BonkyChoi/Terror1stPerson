using UnityEngine;

public class PuertaT : MonoBehaviour
{
    public Animator anim;

    private int objetosDentro = 0;
    private bool enAnimacion = false;
    private bool estadoPuerta = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            objetosDentro++;
            EvaluarPuerta();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            objetosDentro = Mathf.Max(0, objetosDentro - 1);
            EvaluarPuerta();
        }
    }
    private void EvaluarPuerta()
    {
        if (enAnimacion) return;

        bool debeEstarAbierta = objetosDentro > 0;

        if (debeEstarAbierta != estadoPuerta)
        {
            estadoPuerta = debeEstarAbierta;
            enAnimacion = true;

            anim.SetBool("PuertaActiv", estadoPuerta);
        }
    }
    public void FinAnimacion()
    {
        enAnimacion = false;
        EvaluarPuerta();
    }
}