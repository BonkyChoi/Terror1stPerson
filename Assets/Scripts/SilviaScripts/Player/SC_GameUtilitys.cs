using Unity.VisualScripting;
using UnityEngine;


public static class SC_GameUtilitys
{
    private static SC_PlayerController playerController;
    public static SC_PlayerController GetPlayerController()
    {
        SC_PlayerController playerController = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<SC_PlayerController>();
        return playerController;
        
    }
    
    //falta funcion que bindee una accion 

    public static void EnableBeginMiniGame()
    {
        playerController.EnableBeginMiniGame();
    }
}
