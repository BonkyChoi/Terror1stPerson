using System;
using UnityEngine;

namespace SilviaScripts.Enemy
{
    public class SC_PipeTrigger:MonoBehaviour
    {
        private SC_PerceptionSystem _perceptionSystem;

        private void Awake()
        {
            _perceptionSystem = GetComponent<SC_PerceptionSystem>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _perceptionSystem.OnTriggerEnter(other);
        }
    }
}