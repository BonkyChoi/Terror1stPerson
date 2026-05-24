using UnityEngine;

public class Destruible : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    [Header("Audio")]
    [SerializeField] private AudioClip destroySound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains(prefab.name))
        {
            if (destroySound != null)
            {
                AudioSource.PlayClipAtPoint(destroySound, transform.position);
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}