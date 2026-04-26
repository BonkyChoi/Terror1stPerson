using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SC_ChaseAttackState : SC_State
{
    
    //hay que añadir que te persiga con la mirada para que no te pierda tan rapido
    private GameObject target => SC_PlayerHealth.player;
    
    [SerializeField] private float currentVelocity; 
    [SerializeField] private float sprintFloat; 
    private float sprintDistance; 
    
    private float distance; 
    
    //[SerializeField] private LayerMask isAPlayer;
    
    [SerializeField] Animator animator;
    private bool canAttack = false;
    private float sightLostTimer;
    [SerializeField] private float sightLostMaxTime = 2f;


    protected override void Awake()
    {
        base.Awake();
        agent.speed = currentVelocity;
        
    }

    
    
    private IEnumerator MakeRwarBeforeGo()//te avisa de que te ha visto
    {
        Debug.Log("Esperame tantito");
        yield return new WaitForSeconds(0.01f);
        canAttack = true;
        Debug.Log("Te puedo atacar");
        
    }

    public override void OnEnterState(SC_FSMController fsmController)
    {
        myController = fsmController;
        agent.isStopped = false;
        StartCoroutine(MakeRwarBeforeGo());
    }

    public override void OnUpdateState()
    {
        //cual es la distancia entre el player y yo
        ////cual es la distancia a la que el enemigo debe acelerar [la distancia a la que se para + sprint float]
        //hacer el if de la distancia
        //volver a ver el lugar al que ir
        // if (!canAttack)
        // {
        //     Debug.Log("No m da la gana atacars");
        //     return;
        // }
        if (!target)
        {
            Debug.Log("No se ha encontrado el target");
            return;
        }
        FaceToTarget();
        agent.updateRotation = false;
        
        distance = Vector3.Distance(transform.position, target.transform.position);
        sprintDistance = agent.stoppingDistance + sprintFloat;
        
        // if (Physics.Raycast(transform.position,
        //         directionToTarget.normalized, sensorSystem.VisionDistance, isAPlayer))
        // {
            if (distance <= sprintDistance)
            {
                //animator.SetBool("attackPlayer", true);
                agent.speed = currentVelocity * 3;
            }
            else
            {
                //animator.SetBool("attackPlayer", false);
                agent.speed = currentVelocity;
            }

        
        //
        if (!perceptionSystem.CanSeePlayer)
        {
            sightLostTimer += Time.deltaTime;

            if (sightLostTimer >= sightLostMaxTime)
            {
                myController.ChangeState(investigateState);
                return;
            }
        }
        else
        {
            sightLostTimer = 0;
        }

        agent.SetDestination(perceptionSystem.LastPlayerPosition);
        
            
        

        //si la distancia es muy larga o tienes obstáculos debe ir a investigar donde revs el último punto donde lo vio
        ////si investigando no lo encuentra vuelve a patrol
    }


    private void OnTriggerStay(Collider other)
      {
          if (other.TryGetComponent<SC_PlayerHealth>(out var playerHealth) && canAttack)
          {
              playerHealth.ReciveDamage();
          }
      }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<SC_PlayerHealth>(out var playerHealth) && canAttack)
        {
            playerHealth.ReciveDamage();
        }
    }
    private void FaceToTarget()
    {
        //Sacar direccion a objetivo (Destino - origer)
        Vector3 targetDirection = (target.transform.position - agent.transform.position).normalized;
        targetDirection.y = 0;
        //Discriminar la y o ponerla a 0
        Quaternion rotationToTarget = Quaternion.LookRotation(targetDirection);
        transform.rotation = rotationToTarget;
        //Se transforma la direcciona rotacio

        //Lw das la rotacion calculada en el paso 3 al transformposition dl enemigo
    }

   
    public override void OnExitState()
    {
        canAttack = false;

        if (agent != null)
        {
            agent.isStopped = true;
            agent.ResetPath();
            agent.updateRotation = true;
        }

        StopAllCoroutines();
    
    } 
            
}
