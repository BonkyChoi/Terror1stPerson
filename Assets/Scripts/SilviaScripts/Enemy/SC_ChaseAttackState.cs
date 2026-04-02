using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SC_ChaseAttackState : SC_State
{
    private GameObject target => SC_PlayerHealth.player;
    private SC_FSMController myController; 
    private NavMeshAgent agent; 
    [SerializeField] private float currentVelocity; 
    [SerializeField] private float sprintFloat; private float distance; 
    private float sprintDistance; 
    private SC_SensorSystem sensorSystem; 
    [SerializeField] private LayerMask isAPlayer;
    private SC_InvestigateState investigateState; 
    private Vector3 lastDestination; 
    public static System.Action<Vector3> OnPlayerLost;
    [SerializeField] Animator animator;
    private bool canAttack = false;
    


    private void Awake()
    {
        investigateState = GetComponent<SC_InvestigateState>(); 
        agent = GetComponent<NavMeshAgent>(); 
        agent.speed = currentVelocity;
        sensorSystem = GetComponent<SC_SensorSystem>();
    }

    private void OnEnable()
    {
        SC_SensorSystem.OnPlayerFound += OnPlayerFound;

    }

    private void OnPlayerFound(GameObject obj)
    {
        sensorSystem.FoundPlayer = false;
        
        StartCoroutine(MakeRwarBeforeGo());
    }


    private void OnDisable()
    {
        SC_SensorSystem.OnPlayerFound -= OnPlayerFound;
    }

    
    private IEnumerator MakeRwarBeforeGo()//te avisa de que te ha visto
    {
        yield return new WaitForSeconds(3f);
        canAttack = true;
        Debug.Log("Te puedo atacar");
    }

    public override void OnEnterState(SC_FSMController fsmController)
    {
        myController = fsmController;
        agent.isStopped = false;
    }

    public override void OnUpdateState()
    {
        //cual es la distancia entre el player y yo
        ////cual es la distancia a la que el enemigo debe acelerar [la distancia a la que se para + sprint float]
        //hacer el if de la distancia
        //volver a ver el lugar al que ir
        if (!canAttack) return;
        if (!target)
        {
            Debug.Log("No se ha encontrado el target");
            return;
        }
        Vector3 directionToTarget = target.transform.position - transform.position;
        distance = Vector3.Distance(transform.position, target.transform.position);
        sprintDistance = agent.stoppingDistance + sprintFloat;
        
        if (Physics.Raycast(transform.position,
                directionToTarget.normalized, sensorSystem.VisionDistance, isAPlayer))
        {
            if (distance <= sprintDistance)
            {
                animator.SetBool("attackPlayer", true);
                agent.speed = currentVelocity * 3;
            }
            else
            {
                animator.SetBool("attackPlayer", false);
                agent.speed = currentVelocity;
            }

            agent.SetDestination(target.transform.position);
            lastDestination = target.transform.position;
        }
        else
        {
            myController.ChangeState(investigateState);
            OnPlayerLost?.Invoke(lastDestination);
        }

        //si la distancia es muy larga o tienes obstáculos debe ir a investigar donde revs el último punto donde lo vio
        ////si investigando no lo encuentra vuelve a patrol
    }


    private void OnTriggerEnter(Collider other)
      {
          if (other.TryGetComponent<SC_PlayerHealth>(out var playerHealth) && canAttack)
          {
              playerHealth.ReciveDamage();
          }
      }

    public override void OnExitState()
    {
        canAttack = false;
        agent.isStopped = true; 
        StopAllCoroutines(); 
        agent.ResetPath();
    } 
            
}
