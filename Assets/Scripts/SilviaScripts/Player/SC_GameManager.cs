
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
   }

   public void OpenUI()
   {
      playerInput.currentActionMap.Disable();
      playerInput.SwitchCurrentActionMap("UI");
      playerInput.currentActionMap.Enable();
      
      print("OPENUI " + playerInput.currentActionMap);
   }

   public void CloseUI()
   {
      playerInput.currentActionMap.Disable();
      playerInput.SwitchCurrentActionMap("Player");
      playerInput.currentActionMap.Enable();
      
      print("CLOSEUI " + playerInput.currentActionMap);
   }
   
   //creo que falla pq uno le dice cierrate minetras el otro le dice q s abra
   
}
