using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image Vida;
    private float vidaActual;
    private float vidaMaxima;
    public float CantidadCuracionItem;
    
    public void UpdatearVida(float vidaActual, float vidaMaxima)
    {
        Vida.fillAmount = vidaActual / vidaMaxima;
        Debug.Log("VIDAUPDATEADA");
    }

    
    //por si le metemos curación
    public void AumentarVida()
    {
        vidaActual += CantidadCuracionItem;
        if (vidaActual > vidaMaxima)
        {
            vidaActual = vidaMaxima;
        }
        
        Vida.fillAmount = vidaActual / vidaMaxima;
        Debug.Log("A");
    }
}

