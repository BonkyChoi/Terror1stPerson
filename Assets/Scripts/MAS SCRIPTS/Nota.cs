using UnityEngine;

public class Nota : MonoBehaviour
{
    public GameObject panel;

    public void Interact()
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void ClosePanel()
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
        }
    }
}