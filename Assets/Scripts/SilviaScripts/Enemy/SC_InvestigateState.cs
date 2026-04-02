using UnityEngine;
using UnityEngine.AI;

public class SC_InvestigateState : SC_State
{
    //Aparece cuando pierde de vista al jugador mientras esta en chase state, o cuando escucha un sonido sospechoso mientras esta en patrol state
    //Debe buscar el origen del sonido, si encuentra algo sospechoso, cambiar a chase state, sino volver a patrol state

    private SC_FSMController myController;
    private SC_SensorSystem sensorSystem;
    private NavMeshAgent agent;
    private GameObject target => SC_PlayerHealth.player;
    private SC_PatrolState patrols;
    private SC_ChaseAttackState chase;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        sensorSystem = GetComponent<SC_SensorSystem>();
        myController = GetComponent<SC_FSMController>();
        agent = GetComponent<NavMeshAgent>();
        patrols = GetComponent<SC_PatrolState>();
        chase = GetComponent<SC_ChaseAttackState>();
    }

    public override void OnEnterState(SC_FSMController fsmController)
    {
        myController = fsmController;
        //animator.SetTrigger("lookAround");
    }

    public override void OnUpdateState()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            myController.ChangeState(patrols);
        }
    }

    public override void OnExitState()
    {
        agent.isStopped = true;
        //target = null;
        StopAllCoroutines();
        agent.ResetPath();
    }

    private void OnEnable()
    {
        SC_ChaseAttackState.OnPlayerLost += OnPlayerLost;
        SC_SensorSystem.OnPlayerFound += OnPlayerFound;
    }

    private void OnPlayerFound(GameObject obj)
    {
        sensorSystem.FoundPlayer = true;
        myController.ChangeState(chase);
    }

    private void OnPlayerFound()
    {
        
    }

    private void OnPlayerLost(Vector3 lastPlayerPosition)
    {
        //StartCoroutine(SearchPlayer(lastPlayerPosition));
        sensorSystem.FoundPlayer = false;
        agent.SetDestination(sensorSystem.LastPlayerPosition);
    }
}
