using System;
using System.Collections;
using UnityEngine;

public class SC_PatrolState : SC_State
{
    //Inicia el estado de patrulla, el enemigo se movera entre puntos de patrulla predefinidos
    
    private SC_FSMController myController;
    public override void EnterState(SC_FSMController controller)
    {
        myController = controller;
         // Code to execute when entering the state
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        // Code to execute when exiting the state
    }

    private void OnEnable()
    {
        SC_LightManager.OnSwitchOn += PatrolAndWait;
        SC_LightManager.OnSwitchOff +
    }
    private void OnDisable()
    {
        SC_LightManager.OnSwitchOn -= PatrolAndWait;
    }

    private void PatrolAndWait()
    {
        StartCoroutine(PatrolAndWaitCoroutine());
    }

    private IEnumerator PatrolAndWaitCoroutine()
    {
        throw new NotImplementedException();
    }

}