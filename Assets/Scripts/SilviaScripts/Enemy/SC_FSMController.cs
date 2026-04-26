using UnityEngine;

public class SC_FSMController : MonoBehaviour
{
    private SC_State currentState;
    private SC_PatrolState PatrolState { get; set; } //se puede acceder a este estado desde otras clases y modificarlo
    private SC_ChaseAttackState ChaseState { get; set; }
    private SC_InvestigateState InvestigateState { get; set; }
    private SC_PerceptionSystem PerceptionSystem { get; set; }
    
    
    private void Awake()
    {
        PatrolState = GetComponent<SC_PatrolState>();
        ChaseState = GetComponent<SC_ChaseAttackState>();
        InvestigateState = GetComponent<SC_InvestigateState>();
        PerceptionSystem = GetComponent<SC_PerceptionSystem>();
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
