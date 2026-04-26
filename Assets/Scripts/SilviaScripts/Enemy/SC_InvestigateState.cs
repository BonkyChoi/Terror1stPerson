using System;
using UnityEngine;
using UnityEngine.AI;

public class SC_InvestigateState : SC_State
{
    //Aparece cuando pierde de vista al jugador mientras esta en chase state, o cuando escucha un sonido sospechoso mientras esta en patrol state
    //Debe buscar el origen del sonido, si encuentra algo sospechoso, cambiar a chase state, sino volver a patrol state

    
    private GameObject target => SC_PlayerHealth.player;
    
    [SerializeField] private Animator animator;

    [SerializeField] private float maxTimer;
    private float timer;


    protected override void Awake()
    {
        base.Awake();
        timer = maxTimer;
    }

    public override void OnEnterState(SC_FSMController fsmController)
    {
        myController = fsmController;
        timer = maxTimer;
        //animator.SetTrigger("lookAround");
        agent.isStopped = false;
        agent.SetDestination(perceptionSystem.LastPlayerPosition);
    }

    public override void OnEnterState()
    {
        throw new NotImplementedException();
    }

    public override void OnUpdateState()
    {
        if (perceptionSystem.CanSeePlayer)
        {
            myController.ChangeState(chaseState);
            return;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                myController.ChangeState(chaseState);
            }
        }
    }

    public override void OnExitState()
    {
        agent.isStopped = true;
        //target = null;
        StopAllCoroutines();
        agent.ResetPath();
    }

   
}
