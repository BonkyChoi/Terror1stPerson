using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class SC_State
{
    public abstract void OnEnterState();

    public abstract void OnUpdateState();

    public abstract void OnExitState();
    
    
}
