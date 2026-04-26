using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA_Patrol : IA_EnemyStates
{
    //Inicia el estado de patrulla, el enemigo se movera entre puntos de patrulla predefinidos
    

    private List<Vector3> points;
    private int currentPatrolPoint = 0;
    private bool patrolActive;
    
    private SC_FSMController controller;
    private NavMeshAgent agent;
    private SC_PerceptionSystem perception;
    
    private float waitTimer;
    private float waitDuration;
    private bool waiting;

    public IA_Patrol(SC_FSMController controller, List<Vector3> patrolPoints)
    {
        this.controller = controller;
        points = patrolPoints;
        agent = controller.Agent;
        perception = controller.PerceptionSystem;
    }


    private void GoToNextPoint()
    {
        if (points.Count == 0) return;
        agent.SetDestination(points[currentPatrolPoint]);
    }

    public override void OnEnterState()
    {
        currentPatrolPoint = 0;
        waiting = false;
        GoToNextPoint();
    }

    public override void OnUpdateState()
    {
        if (perception.CanSeePlayer)
        {
            controller.ChangeState(controller.Chase);
            return;
        }
        if (perception.CanHearPlayer)
        {
            controller.ChangeState(controller.Investigate);
            return;
        }
        if (!waiting)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                waiting = true;
                waitDuration = Random.Range(0.5f, 1.5f);
                waitTimer = 0f;
            }
        }
        else
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitDuration)
            {
                waiting = false;
                currentPatrolPoint = (currentPatrolPoint + 1) % points.Count;
                GoToNextPoint();
            }
        }
    }

    public override void OnExitState()
    {
        agent.ResetPath();
    }

}
