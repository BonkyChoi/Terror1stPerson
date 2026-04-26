using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SC_FSMController : MonoBehaviour
{
    private IA_EnemyStates currentState;
    private IA_Patrol PatrolState; //se puede acceder a este estado desde otras clases y modificarlo
    private IA_Chase ChaseState;
    private IA_Investigate InvestigateState;
    public SC_PerceptionSystem PerceptionSystem { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    
    //Patrol
    private List<Vector3> patrolPoints = new();
    [SerializeField] private Transform patrolRoute;
    
    
    //Attack
    [SerializeField] private CapsuleCollider triggerCollider;
    
    //Animator
    [SerializeField] private Animator animator;
    
    
    
    
    
    private void Awake()
    {
        triggerCollider.enabled = true;
        PerceptionSystem = GetComponent<SC_PerceptionSystem>();
        Agent = GetComponent<NavMeshAgent>();
        //Patrol
        foreach (Transform points in patrolRoute)
        {
            patrolPoints.Add(points.position);
        }
        ChaseState = new IA_Chase(this);
        PatrolState = new IA_Patrol(this, patrolPoints);
        InvestigateState = new IA_Investigate(this);

    }
    
    private void Update()
    {
        currentState?.OnUpdateState();
        
    }

    public void ChangeState(IA_EnemyStates newState)
    {
        if (currentState == newState) return;
        currentState?.OnExitState();
        currentState = newState;
        currentState.OnEnterState();

    }
    
    //__PATROL_
    
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
    
    //__CHASE__
    
    private void OnTriggerStay(Collider other)
    {
        
        if (other.TryGetComponent<SC_PlayerHealth>(out var playerHealth))
        {
            playerHealth.ReciveDamage();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        
        if (other.TryGetComponent<SC_PlayerHealth>(out var playerHealth)) 
        {
            playerHealth.ReciveDamage();
            
        }
        
        
    }
    
    public Coroutine RunCoroutine(IEnumerator routine)//le permite al estado realizar lo que necesita
    {
        return StartCoroutine(routine);
    }

    public Coroutine StopCoroutine(IEnumerator routine)
    {
        return StopCoroutine(routine);
    }

    public IA_EnemyStates Patrol => PatrolState;
    public IA_EnemyStates Chase => ChaseState;
    public IA_EnemyStates Investigate => InvestigateState;
    
    
}
