using System;
using UnityEngine;

public class SC_LInter2Tutorial : MonoBehaviour
{
    [SerializeField] private Light lintern;

    private void Awake()
    {
        lintern.enabled = true;
    }
}
