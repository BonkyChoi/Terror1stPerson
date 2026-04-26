using UnityEngine;

public class Nota : MonoBehaviour
{
    public GameObject panel;

    private bool playerInside = false;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            
            if (panel.activeSelf)
            {
                panel.SetActive(false);
            }
        }
    }
}