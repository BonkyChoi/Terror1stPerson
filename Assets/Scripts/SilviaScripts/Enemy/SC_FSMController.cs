using UnityEngine;

public class SC_FSMController : MonoBehaviour
{
    private SC_State currentState;
    private SC_PatrolState PatrolState { get; set; } //se puede acceder a este estado desde otras clases y modificarlo
    
    private void Awake()
    {
        PatrolState = GetComponent<SC_PatrolState>();
        ChangeState(PatrolState);
        
    }

    internal void ChangeState(SC_State newState)
    {
        currentState?.OnExitState();
        currentState = newState;
        currentState.OnEnterState(this);

    }

    private void Update()
    {
        currentState.OnUpdateState();
    }
    
    
}
