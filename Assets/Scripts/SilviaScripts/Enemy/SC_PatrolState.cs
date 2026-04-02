using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class SC_PatrolState : SC_State
{
    //Inicia el estado de patrulla, el enemigo se movera entre puntos de patrulla predefinidos
    
    private SC_FSMController myController;
    private SC_ChaseAttackState chaseAttackState;
    
    private NavMeshAgent agent;

    [SerializeField]private Transform patrolRoute;
    private List<Vector3> patrolPoints = new();
    private int currentPatrolPoint = 0;
    private bool patrolActive;

    private void Awake()
    {
        chaseAttackState = GetComponent<SC_ChaseAttackState>();
        agent = GetComponent<NavMeshAgent>();
        myController = GetComponent<SC_FSMController>();

        foreach (Transform points in patrolRoute)
        {
            patrolPoints.Add(points.position);
        }
    }

    public override void OnEnterState(SC_FSMController controller)
    {
        myController = controller;
         // Code to execute when entering the state
    }

    public override void OnUpdateState()
    {
        //nothing now
    }

    public override void OnExitState()
    {
        patrolActive = false;
        
        agent.ResetPath();
        StopAllCoroutines();
    }

    private void OnEnable()
    {
        SC_LightManager.OnSwitchOff += PatrolAndWait;
        SC_LightManager.OnSwitchOn += StopMovement;
        SC_SensorSystem.OnPlayerFound += OnPlayerFound;
    }

    private void OnPlayerFound(GameObject obj)
    {
        Debug.Log("ChangeState>ToAttack");
        myController.ChangeState(chaseAttackState);
    }

    

    private void StopMovement()
    {
        patrolActive = false;
        agent.isStopped = true;
    }

    private void OnDisable()
    {
        SC_LightManager.OnSwitchOff -= PatrolAndWait;
        SC_LightManager.OnSwitchOn -= StopMovement;
    }

    private void PatrolAndWait()
    {
        patrolActive = true;
        StartCoroutine(PatrolAndWaitCoroutine());
    }

    private IEnumerator PatrolAndWaitCoroutine()
    {
        //rwarrr!
        yield return new WaitForSeconds(1);
        //ahora ya se mueve
        
        while (patrolActive)
        {
            agent.isStopped = false;
            agent.SetDestination(patrolPoints[currentPatrolPoint]);
            yield return new WaitUntil(ReachedDestination);
            yield return new WaitForSeconds(Random.Range(0.2f, 1.5f));
            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Count;

        }

    }

    private bool ReachedDestination()
    {
        return !agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance; //si no tienes pendiente un camino y la distancia
        //que queda entre el punto y tu parada
    }
}