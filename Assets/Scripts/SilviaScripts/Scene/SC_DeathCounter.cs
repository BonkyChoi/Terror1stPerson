using System;
using UnityEngine;

public class SC_DeathCounter : MonoBehaviour
{
    public static SC_DeathCounter Instance;
    public int DeathCounter {get; set;}

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
