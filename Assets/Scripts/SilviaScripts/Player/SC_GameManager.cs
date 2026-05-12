
using UnityEngine;
using UnityEngine.InputSystem;

public class SC_GameManager : MonoBehaviour
{
   public static SC_GameManager Instance {get; private set;}
   private  PlayerInput playerInput;

   private void Awake()
   {
      if (Instance != this && Instance != null)
      {
         Destroy(gameObject);
         return;
      }
      Instance = this;
      DontDestroyOnLoad(gameObject);
      //playerInput = FindFirstObjectByType<PlayerInput>();
      //playerInput = GetComponent<PlayerInput>();
   }
   
   public void RegisterPlayerInput(PlayerInput input)
   {
      playerInput = input;
      Debug.Log("RegisterPlayerInput");
   }

   public void OpenUI()
   {
      playerInput.SwitchCurrentActionMap("UI");
      Debug.Log("OpenUILOL");
   }

   public void CloseUI()
   {
      playerInput.SwitchCurrentActionMap("Player");
      Debug.Log("OpenPlayerssLOL");
   }
   
}
