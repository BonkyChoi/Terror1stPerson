using UnityEngine;
using UnityEngine.InputSystem;

public class SC_PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    
    //hacer public void de acctivar el input 

    public void DisableBeginMiniGame()
    {
        playerInput.actions["BeginMiniGame"].Disable();
    }
    
    public void EnableBeginMiniGame()
    {
        playerInput.actions["BeginMiniGame"].Enable();
    }
    
    
}
