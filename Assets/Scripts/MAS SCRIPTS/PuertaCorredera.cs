using UnityEngine;

public class PuertaCorredera : MonoBehaviour
{
    public Transform doorVisual;
    public Transform openPoint;
    public Transform closedPoint;

    public float speed = 5f;
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;

    private bool isOpen = false;

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

        bool shouldBeOpen = (target == openPoint.position);

        if (shouldBeOpen && !isOpen)
        {
            isOpen = true;

            if (openSound != null)
                audioSource.PlayOneShot(openSound);
        }
        else if (!shouldBeOpen && isOpen)
        {
            isOpen = false;

            if (closeSound != null)
                audioSource.PlayOneShot(closeSound);
        }
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