using System;
using UnityEngine;
using UnityEngine.AI;

public class SC_InvestigateState : SC_State
{
    //Aparece cuando pierde de vista al jugador mientras esta en chase state, o cuando escucha un sonido sospechoso mientras esta en patrol state
    //Debe buscar el origen del sonido, si encuentra algo sospechoso, cambiar a chase state, sino volver a patrol state

    
    private GameObject target => SC_PlayerHealth.player;
    private Transform transform => controller.transform;
    
    [SerializeField] private Animator animator;

    [SerializeField] private float maxTimer;
    private float timer;
    
    private SC_FSMController controller;
    private NavMeshAgent agent;
    private SC_PerceptionSystem perception;

    public SC_InvestigateState(SC_FSMController controller)
    {
        this.controller = controller;
        
        agent = controller.Agent;
        perception = controller.PerceptionSystem;
    }

    public override void OnEnterState()
    {
        timer = maxTimer;
        //animator.SetTrigger("lookAround");
        agent.isStopped = false;
        agent.SetDestination(perception.LastPlayerPosition);
    }
    

    public override void OnUpdateState()
    {
        if (perception.CanSeePlayer)
        {
            controller.ChangeState(controller.Chase);
            return;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                controller.ChangeState(controller.Patrol);
            }
        }
    }

    public override void OnExitState()
    {
        agent.isStopped = true;
        //target = null;
        controller.StopAllCoroutines();
        agent.ResetPath();
    }

   
}
