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
    
    private NavMeshAgent agent;

    private Transform patrolRoute;
    private List<Vector3> patrolPoints = new();
    private int currentPatrolPoint = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        foreach (Transform points in patrolRoute)
        {
            patrolPoints.Add(points.position);
        }
    }

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
        SC_LightManager.OnSwitchOff += StopMovement;
    }

    private void StopMovement()
    {
        agent.enabled = false;
    }

    private void OnDisable()
    {
        SC_LightManager.OnSwitchOn -= PatrolAndWait;
        SC_LightManager.OnSwitchOff -= StopMovement;
    }

    private void PatrolAndWait()
    {
        StartCoroutine(PatrolAndWaitCoroutine());
    }

    private IEnumerator PatrolAndWaitCoroutine()
    {
        while (true)
        {
            agent.enabled = true;
            agent.SetDestination(patrolPoints[currentPatrolPoint]);
            yield return new WaitUntil(ReachedDestination);
            yield return new WaitForSeconds(Random.Range(0.2f, 1.5f));

        }
        
    }

    private bool ReachedDestination()
    {
        return !agent.pathPending &&
            agent.remainingDistance >= agent.stoppingDistance; //si no tienes pendiente un camino y la distancia
        //que queda entre el punto y tu parada
    }
}