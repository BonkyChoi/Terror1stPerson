using UnityEngine;
using UnityEngine.SceneManagement;

public class Titulo : MonoBehaviour
{
    public void JUGAR()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SALIR()
    {
        Application.Quit();
    }
}
