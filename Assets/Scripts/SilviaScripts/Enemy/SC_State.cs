using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class SC_State : MonoBehaviour
{
    public abstract void OnEnterState(SC_FSMController controller);

    public abstract void OnUpdateState();

    public abstract void OnExitState();
    
    protected SC_PatrolState patrolState;
    protected SC_ChaseAttackState  chaseState;
    protected SC_InvestigateState investigateState;
    protected SC_PerceptionSystem perceptionSystem;
    protected SC_FSMController myController;
    protected NavMeshAgent agent;

    protected virtual void Awake()
    {
        patrolState = GetComponent<SC_PatrolState>();
        chaseState = GetComponent<SC_ChaseAttackState>();
        investigateState = GetComponent<SC_InvestigateState>();
        perceptionSystem = GetComponent<SC_PerceptionSystem>();
        myController = GetComponent<SC_FSMController>();
        agent = GetComponent<NavMeshAgent>();
    }
}
