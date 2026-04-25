using System;
using UnityEngine;

public class SC_StepsSound : MonoBehaviour
{ 
   [SerializeField] private SphereCollider stepsDetector;
   [SerializeField] private float moveRadius = 1.5f;
   [SerializeField] private float runRadius = 2.2f;
   [SerializeField] private float crouchRadius = 1f;

   private void OnEnable()
   {
      PlayerMovementV.OnMovementChanged += OnMovementChanged;
   }

   private void OnMovementChanged(string movement)
   {
      switch (movement)
      {
         case "moving":
            stepsDetector.radius = moveRadius;
            break;
         case "crouching":
            stepsDetector.radius = crouchRadius;
            break;
         case "running":
            stepsDetector.radius = runRadius;
            break;
      }
   }

   private void OnDisable()
   {
      PlayerMovementV.OnMovementChanged -= OnMovementChanged;
   }
}
