using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SC_FSMController : MonoBehaviour
{
    private SC_State currentState;
    private SC_PatrolState PatrolState; //se puede acceder a este estado desde otras clases y modificarlo
    private SC_ChaseAttackState ChaseState;
    private SC_InvestigateState InvestigateState;
    public SC_PerceptionSystem PerceptionSystem { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    
    //Patrol
    private List<Vector3> patrolPoints = new();
    [SerializeField] private Transform patrolRoute;
    
    
    //Attack
    [SerializeField] private SphereCollider triggerCollider;
    
    
    
    private void Awake()
    {
        
        PerceptionSystem = GetComponent<SC_PerceptionSystem>();
        Agent = GetComponent<NavMeshAgent>();
        //Patrol
        foreach (Transform points in patrolRoute)
        {
            patrolPoints.Add(points.position);
        }
        ChaseState = new SC_ChaseAttackState();
        PatrolState = new SC_PatrolState(this, patrolPoints);
        InvestigateState = new SC_InvestigateState();
        
        
    }
    
    private void Update()
    {
        currentState?.OnUpdateState();
        
    }

    public void ChangeState(SC_State newState)
    {
        if (currentState == newState) return;
        currentState?.OnExitState();
        currentState = newState;
        currentState.OnEnterState(this);

    }
    
    private void OnEnable()
    {
        SC_LightManager.OnSwitchOff += PatrolAndWait;
        SC_LightManager.OnSwitchOn += StopMovement;
        
    }

    private void StopMovement()
    {
        //bloquear al agente
        Agent.isStopped = true;
        triggerCollider.enabled = false;
    }

    private void PatrolAndWait()
    {
        ChangeState(PatrolState);
        triggerCollider.enabled = true;
    }

    private void OnDisable()
    {
        SC_LightManager.OnSwitchOff -= PatrolAndWait;
        SC_LightManager.OnSwitchOn -= StopMovement;
    }

    public SC_State Patrol => PatrolState;
    public SC_State Chase => ChaseState;
    public SC_State Investigate => InvestigateState;
    
    
}
