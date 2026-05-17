using System.Collections;
using UnityEngine;

public class SC_ToOpenFirstDoor : MonoBehaviour
{
   [SerializeField] private Transform pivot;
   [SerializeField] private float openAngle = -90f;
   [SerializeField] private float speed = 2f;

   private bool opened;

   public void OpenDoor()
   {
      if (opened) return;

      StartCoroutine(OpenDoorCoroutine());
   }

   private IEnumerator OpenDoorCoroutine()
   {
      opened = true;

      Quaternion targetRotation =
         Quaternion.Euler(0, openAngle, 0) * pivot.rotation;

      while (Quaternion.Angle(pivot.rotation, targetRotation) > 0.1f)
      {
         pivot.rotation = Quaternion.Slerp(
            pivot.rotation,
            targetRotation,
            speed * Time.deltaTime
         );

         yield return null;
      }

      pivot.rotation = targetRotation;
   }

}
