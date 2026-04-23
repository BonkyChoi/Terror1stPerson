using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CajaFuerte : MonoBehaviour
{
    [Header("Animacion")]
    public Animator anim;

    [Header("UI")]
    public GameObject panelCodigo;
    public Image[] indicadores;

    [Header("Codigo")]
    public int[] codigoCorrecto = new int[4];

    private bool enZona = false;
    private bool panelActivo = false;
    private bool enAnimacion = false;
    private bool enProceso = false;

    private int[] codigoActual = new int[4];
    private int indice = 0;

    private void Start()
    {
        panelCodigo.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && enZona && !enAnimacion)
        {
            if (panelActivo)
                CerrarPanel();
            else
                AbrirPanel();
        }
    }
    void AbrirPanel()
    {
        panelActivo = true;
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
        if (!panelActivo || enProceso) return;

        codigoActual[indice] = numero;
        indice++;

        if (indice == 4)
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
        enAnimacion = true;
        anim.SetBool("PuertaActiv", true);
        Destroy(panelCodigo);
    }
    public void FinAnimacion()
    {
        enAnimacion = false;
    }
    private void OnTriggerEnter(Collider other)
    {
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