using UnityEngine;

public class Destruible : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains(prefab.name))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}