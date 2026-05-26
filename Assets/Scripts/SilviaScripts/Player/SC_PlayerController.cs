using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SC_PlayerController : MonoBehaviour
{
    //public static SC_PlayerController Instance;
    [SerializeField] private PlayerInput playerInput;
    
    //hacer public void de acctivar el input 

    /*
    public void DisableBeginMiniGame()
    {
        playerInput.actions["BeginMiniGame"].Disable();
    }
    
    public void EnableBeginMiniGame()
    {
        playerInput.actions["BeginMiniGame"].Enable();
    }

    public void EnablePauseCursor()
    {
        playerInput.actions["PauseCursor"].Enable();
        
    }
    */
    
    private void Awake()
    {
        // if (Instance != this && Instance != null)
        // {
        //     Destroy(gameObject);
        //     return;
        // }
        // Instance = this;
        // DontDestroyOnLoad(gameObject);
        //playerInput = FindFirstObjectByType<PlayerInput>();
        //playerInput = GetComponent<PlayerInput>();
    }//

    private void Start()
    {
        SC_GameManager.Instance.RegisterPlayerInput(playerInput);
        print("He registrado al playerInput");
    }


    private void OnEnable()
    {
        playerInput.actions["BeginMiniGame"].started += BeginMiniGame;
        playerInput.actions["PauseCursor"].started += PauseCursor;
    }

    private void OnDestroy()
    {
        if (playerInput == null) return;

        playerInput.actions["BeginMiniGame"].started -= BeginMiniGame;
        playerInput.actions["PauseCursor"].started -= PauseCursor;
    }

    private void BeginMiniGame(InputAction.CallbackContext ctx)
    {
        print("Call begin mini game");
        //una vez que muere no llama a esto
        SC_InputEvent.OnBeginMiniGame?.Invoke();
    }

    private void PauseCursor(InputAction.CallbackContext ctx)
    {
        SC_InputEvent.OnPauseCursor?.Invoke();
    }
}
