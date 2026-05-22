using UnityEngine;

public class PuertaCorredera : MonoBehaviour
{
    public Transform doorVisual;
    public Transform openPoint;
    public Transform closedPoint;

    public float speed = 5f;

    private int insideCount = 0;
    private Vector3 target;

    void Start()
    {
        target = closedPoint.position;
    }
    void Update()
    {
        doorVisual.position = Vector3.MoveTowards(
            doorVisual.position,
            target,
            speed * Time.deltaTime
        );
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            insideCount++;
            target = openPoint.position;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            insideCount--;

            if (insideCount <= 0)
                target = closedPoint.position;
        }
    }
}