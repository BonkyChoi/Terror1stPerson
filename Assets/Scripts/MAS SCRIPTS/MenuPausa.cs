using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject[] otherPanels;

    bool isMenuOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (AreOtherPanelsOpen())
                return;

            if (isMenuOpen)
                CloseMenu();
            else
                OpenMenu();
        }
    }
    bool AreOtherPanelsOpen()
    {
        foreach (GameObject panel in otherPanels)
        {
            if (panel.activeSelf)
                return true;
        }
        return false;
    }
    void OpenMenu()
    {
        pauseMenu.SetActive(true);
        isMenuOpen = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void CloseMenu()
    {
        pauseMenu.SetActive(false);
        isMenuOpen = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("Titulo");
    }
}