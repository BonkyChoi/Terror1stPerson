using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SC_PlayerBrain : MonoBehaviour
{
    // Se utilizará para tomar las decisiones de los input mapping
    private PlayerInput playerInput;
    
    public static Action OnInteract;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerInput.actions["Interact"].started += OnInteractionStarted;
    }

    private void OnDisable()
    {
        playerInput.actions["Interact"].started -= OnInteractionStarted;
    }
    private void OnInteractionStarted(InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke();
    }


}
