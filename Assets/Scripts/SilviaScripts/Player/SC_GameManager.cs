using UnityEngine;
using UnityEngine.InputSystem;

public class SC_GameManager : MonoBehaviour
{
   public static SC_GameManager Instance {get; private set;}
   [SerializeField] private  PlayerInput playerInput;

   private void Awake()
   {
      if (Instance != this && Instance != null)
      {
         Destroy(gameObject);
         return;
      }
      Instance = this;
      DontDestroyOnLoad(gameObject);
      //playerInput = GetComponent<PlayerInput>();
   }

   public void OpenUI()
   {
      playerInput.SwitchCurrentActionMap("UI");
   }

   public void CloseUI()
   {
      playerInput.SwitchCurrentActionMap("Player");
   }
   
}
