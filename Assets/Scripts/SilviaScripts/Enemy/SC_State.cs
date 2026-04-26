using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class SC_State
{
    public abstract void OnEnterState();

    public abstract void OnUpdateState();

    public abstract void OnExitState();
    
    //da errores pq antes estaba construido como monobehavior :c, nos cambiamos a los "IA_tatata"
    
    
}
