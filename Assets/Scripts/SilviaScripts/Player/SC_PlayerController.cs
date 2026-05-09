using UnityEngine;
using UnityEngine.InputSystem;

public class SC_PlayerController : MonoBehaviour
{
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
    
    private void OnEnable()
    {
        playerInput.actions["BeginMiniGame"].started += BeginMiniGame;
        playerInput.actions["PauseCursor"].started += PauseCursor;
    }

    private void OnDisable()
    {
        playerInput.actions["BeginMiniGame"].started -= BeginMiniGame;
        playerInput.actions["PauseCursor"].started -= PauseCursor;
    }

    private void BeginMiniGame(InputAction.CallbackContext ctx)
    {
        SC_InputEvent.OnBeginMiniGame?.Invoke();
    }

    private void PauseCursor(InputAction.CallbackContext ctx)
    {
        SC_InputEvent.OnPauseCursor?.Invoke();
    }
    
  

    
    
}
