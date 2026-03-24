using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_PlayerHealth : MonoBehaviour
{
    public static GameObject player;

    private int health = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReciveDamage()
    {
        health--;
        if (health <= 0)
        {
            SceneManager.LoadScene("BadEnding");
        }
    }
}
