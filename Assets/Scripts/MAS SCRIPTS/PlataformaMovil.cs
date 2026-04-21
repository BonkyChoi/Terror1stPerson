using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;
    public float velocidad = 2f;

    private bool irAPuntoB;

    void Start()
    {
        transform.position = puntoA.position;
    }

    void Update()
    {
        Transform objetivo = irAPuntoB ? puntoB : puntoA;

        transform.position = Vector3.MoveTowards(
            transform.position,
            objetivo.position,
            velocidad * Time.deltaTime
        );
    }

    public void Activar()
    {
        irAPuntoB = !irAPuntoB;
        Debug.Log("Plataforma activada: " + gameObject.name);
    }
}