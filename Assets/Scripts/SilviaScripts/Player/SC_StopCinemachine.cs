using System;
using Unity.Cinemachine;
using UnityEngine;

namespace SilviaScripts.Player
{
    public class SC_StopCinemachine:MonoBehaviour
    {
        [SerializeField] private CinemachineCamera cam;

        private void Awake()
        {
            cam.enabled = false;
        }
    }
}