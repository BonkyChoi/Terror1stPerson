using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SC_PerceptionSystem : MonoBehaviour
{
    //El oido percibe lo que lleve la tag asi que sirve tambien para objetos que tirar y asi despistar
    public bool CanHearPlayer { get; set; }
    public bool CanSeePlayer{ get; set; }
    
    public Vector3 LastPlayerPosition { get; set; }
    
    private GameObject target => SC_PlayerHealth.player;

    private bool canPercive;
    
    private Coroutine hearCoroutine;
    private Coroutine seeCoroutine;
    
    
    //--OidoReferencias--
    // Cuando detecta un sonido mira al lugar de donde ha surgido
    //Lo de las tuberías se hará con waypoints en el lugar que hagan de zona donde finalice la tubería. Estas tuberías iran con eventos y
    //cuando detecten un sonido mandarán a todas las subscritas si escuchan al player, de escucharlo, le darán la localización al enemigo

    [SerializeField] private SphereCollider monsterEars;//debe determinar el radio de escuha del monstruo
    private NavMeshAgent agent;
    private bool haveATarget;
    private bool listeningCoroutine;
    
    //--VisiónReferencias--
    //solo deberia tener sencor cuando las luces 3sten encendudas

    [field: SerializeField]
    public float
        SensorAngle { get; private set; } //si son 33 grados, el sensor va a ser de 16.5 grados a cada lado del enemigo

    [field: SerializeField]
    public float VisionDistance { get; private set; } //rango de unidades en las que detecta al jugador

    [SerializeField] private float sphereOffset;

    [SerializeField] private LayerMask isAPlayer;
    [SerializeField] private LayerMask isAnObstacle;

    private void OnEnable()
    {
        //Cuando puede comenzar a correr el perception, antes de ello no puede hacer nada
        //Cuando para de correr el perception
        //Esto se mide con las luces
        SC_LightManager.OnSwitchOff += BeginToPercive;
        SC_LightManager.OnSwitchOn += StopToPercive;
    }
    
    private void OnDisable()
    {
        SC_LightManager.OnSwitchOff -= BeginToPercive;
        SC_LightManager.OnSwitchOn -= StopToPercive;
    }

    private void BeginToPercive()
    {
        canPercive = true;
        seeCoroutine = StartCoroutine(EyePerception());

    }
    
    private void StopToPercive()
    {
        canPercive = false;
        StopAllCoroutines();
    }
    
    //--OídoFunciones--
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("DetectorEnemy") && canPercive)
        {
            haveATarget = true;
            if (listeningCoroutine) return;
            listeningCoroutine = true;
            CanHearPlayer = true;
            hearCoroutine = StartCoroutine(InvokeHaveATarget(other.transform));
        }
    }

    private IEnumerator InvokeHaveATarget(Transform otherTransform)
    {
        while (haveATarget)
        {
            LastPlayerPosition = target.transform.position;
            yield return new WaitForSeconds(5);
        }
    }

   
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DetectorEnemy"))
        {
            haveATarget = false;
            StopCoroutine(hearCoroutine);
            listeningCoroutine = false;
            CanHearPlayer = false;
        }
    }
    
    //--OjoFunciones--

    private IEnumerator EyePerception()
    {
        while (canPercive)
        {
            CanSeePlayer = false;

            Vector3 directionToPlayer = target.transform.position - transform.position;
            float distance = directionToPlayer.magnitude;

            if (distance > VisionDistance) yield return null;
            

            float angle = Vector3.Angle(transform.forward, directionToPlayer);

            if (angle > SensorAngle) yield return null;
            

            if (!Physics.Raycast(transform.position, directionToPlayer.normalized, distance, isAnObstacle))
            {
                CanSeePlayer = true;
                LastPlayerPosition = target.transform.position;
            }

            yield return new WaitForSeconds(0.2f);
        }

    }

   

    private void OnDrawGizmos()
    {
        Gizmos.color = new Vector4(1, 0, 0, 0.7f);
        Gizmos.DrawSphere(this.transform.position + this.transform.forward * sphereOffset, VisionDistance);
    }
    
}
