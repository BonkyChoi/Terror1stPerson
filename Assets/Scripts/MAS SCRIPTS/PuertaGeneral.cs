using UnityEngine;

public class PuertaGeneral : MonoBehaviour
{
    public Animator anim;
    
    [Header("Audio")]
    public AudioSource audioSource;

    public AudioClip openSound;
    public AudioClip closeSound;

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

            if (activa)
            {
                if (openSound != null)
                    audioSource.PlayOneShot(openSound);
            }
            else
            {
                if (closeSound != null)
                    audioSource.PlayOneShot(closeSound);
            }
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