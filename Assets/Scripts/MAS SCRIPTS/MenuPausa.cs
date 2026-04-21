using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject pauseMenu;
    bool isMenuOpen = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isMenuOpen)
                CloseMenu();
            else
                OpenMenu();
        }
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
