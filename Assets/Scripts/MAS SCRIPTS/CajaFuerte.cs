using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CajaFuerte : MonoBehaviour
{
    [Header("ANIMACION")]
    public Animator anim;
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip successSound;
    public AudioClip failSound;

    [Header("UI PANEL")]
    public GameObject panelCodigo;
    public Image[] indicadores;

    [Header("CODIGO CORRECTO")]
    public int[] codigoCorrecto = new int[4];

    private int[] codigoActual = new int[4];
    private int indice = 0;

    [Header("ESTADO")]
    private bool enZona = false;
    private bool panelActivo = false;
    private bool enProceso = false;
    private bool desbloqueado = false;

    private void Start()
    {
        panelCodigo.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && enZona && !enProceso)
        {
            if (desbloqueado) return;

            if (panelActivo)
                CerrarPanel();
            else
                AbrirPanel();
        }
    }
    void AbrirPanel()
    {
        panelActivo = true;
        if (!panelCodigo.activeSelf)
            panelCodigo.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Resetear();
    }
    void CerrarPanel()
    {
        panelActivo = false;
        panelCodigo.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void PulsarNumero(int numero)
    {
        if (!panelActivo || enProceso || desbloqueado) return;

        codigoActual[indice] = numero;
        indice++;

        if (indice >= 4)
        {
            StartCoroutine(ComprobarCodigo());
        }
    }
    IEnumerator ComprobarCodigo()
    {
        enProceso = true;

        bool correcto = true;

        for (int i = 0; i < 4; i++)
        {
            if (codigoActual[i] != codigoCorrecto[i])
            {
                correcto = false;
                break;
            }
        }
        for (int i = 0; i < indicadores.Length; i++)
        {
            indicadores[i].color = correcto ? Color.green : Color.red;
        }
        if (correcto)
        {
            if (successSound != null)
                audioSource.PlayOneShot(successSound);
        }
        else
        {
            if (failSound != null)
                audioSource.PlayOneShot(failSound);
        }
        
        yield return new WaitForSeconds(2f);

        if (correcto)
        {
            AbrirCaja();
        }

        CerrarPanel();
        enProceso = false;
    }
    void AbrirCaja()
    {
        desbloqueado = true;
        panelActivo = false;

        anim.SetBool("PuertaActiv", true);
        
        if (openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }
    }
    public void FinAnimacion()
    {
        enProceso = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (desbloqueado) return;

        if (other.CompareTag("Player"))
            enZona = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enZona = false;
            CerrarPanel();
        }
    }
    void Resetear()
    {
        indice = 0;

        for (int i = 0; i < 4; i++)
        {
            codigoActual[i] = 0;
            indicadores[i].color = Color.white;
        }
    }
}