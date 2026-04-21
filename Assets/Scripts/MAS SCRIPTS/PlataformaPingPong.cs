using UnityEngine;

public class PlataformaPingPong : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;
    public float velocidad = 2f;

    private bool activa = false;
    private Vector3 objetivo;

    void Start()
    {
        objetivo = puntoB.position;
    }
    void Update()
    {
        if (!activa) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            objetivo,
            velocidad * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, objetivo) < 0.01f)
        {
            objetivo = (objetivo == puntoA.position) ? puntoB.position : puntoA.position;
        }
    }
    public void ActivarMovimiento()
    {
        activa = true;
    }
}
