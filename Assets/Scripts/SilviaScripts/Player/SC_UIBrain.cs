using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SC_UIBrain : MonoBehaviour
{
    private PlayerInput playerInput;
    public static Action OnExitGame;

    public static Action OnMiniGame;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerInput.actions["BeginMiniGame"].started += OnMiniGameVoid;
        playerInput.actions["ExitMiniGame"].started += OnExitGameVoid;
    }
    private void OnDisable()
    {
        playerInput.actions["BeginMiniGame"].started -= OnMiniGameVoid;
        playerInput.actions["ExitMiniGame"].started -= OnExitGameVoid;
    }
    

    private void OnExitGameVoid(InputAction.CallbackContext obj)
    {
        OnExitGame?.Invoke();
    }

    private void OnMiniGameVoid(InputAction.CallbackContext obj)
    {
        Debug.LogWarning("MiniGame");
        OnMiniGame?.Invoke();
        
    }

    
}

