using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SC_PlayerBrain : MonoBehaviour
{
    // Se utilizará para tomar las decisiones de los input mapping
    private PlayerInput playerInput;
    
    public static Action OnInteract;
    public static Action OnDesinteract;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerInput.actions["Interact"].started += OnInteractionStarted;
        playerInput.actions["Interact"].canceled += OnInteractionCanceled;
    }

    private void OnInteractionCanceled(InputAction.CallbackContext obj)
    {
        OnDesinteract?.Invoke();
    }

    private void OnDisable()
    {
        playerInput.actions["Interact"].started -= OnInteractionStarted;
        playerInput.actions["Interact"].canceled -= OnInteractionCanceled;
    }
    private void OnInteractionStarted(InputAction.CallbackContext obj)
    {
        Debug.Log("OnInteractionStarted");
        OnInteract?.Invoke();
    }
    
}
