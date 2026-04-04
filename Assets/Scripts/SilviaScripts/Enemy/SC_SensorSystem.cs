using System;
using UnityEngine;

public class SC_SensorSystem : MonoBehaviour
{
    
    //solo deberia tener sencor cuando las luces 3sten encendudas

       [field: SerializeField]
    public float
        SensorAngle { get; private set; } //si son 33 grados, el sensor va a ser de 16.5 grados a cada lado del enemigo

    [field: SerializeField]
    public float VisionDistance { get; private set; } //rango de unidades en las que detecta al jugador

    [SerializeField] private float sphereOffset;

    [SerializeField] private LayerMask isAPlayer;
    [SerializeField] private LayerMask isAnObstacle;
    

    private GameObject player; //el player que va a ser enviado a otros scripts
    private bool canSearch;

    //se necesita enviar un evento con una referencia al gameobjet
    public static System.Action<GameObject> OnPlayerFound;
    
    public bool FoundPlayer { get; set; }
    
    public Vector3 LastPlayerPosition { get; set; }
    
    //hacer cono de 33 grados

    private void Awake()
    {
        //if (player != null) Destroy(player);
        
    }


    private void FixedUpdate()
    {
        if (!canSearch) return;
        
        
        Collider[] col = Physics.OverlapSphere(this.transform.position + this.transform.forward * sphereOffset, VisionDistance, isAPlayer);
        
        if (col.Length > 0)
        {
            Vector3 directionToTarget = col[0].transform.position - this.transform.position;
            Debug.DrawRay(transform.position, directionToTarget.normalized, Color.red);
            
            LastPlayerPosition = col[0].transform.position;
            
            //if (FoundPlayer) return;
            
            //if (Vector3.Angle(this.transform.forward, directionToTarget) <=
                // SensorAngle / 2) //tiene que estar en su visión que se //divide entre dos porque son 16.5 para cada lado
            // {
                //comprobar que no haya objetos
                //para eso deebes saber que es un obstáculo
                if (!Physics.Raycast(this.transform.position, directionToTarget.normalized, VisionDistance,
                        isAnObstacle))
                {
                   // if (!FoundPlayer)
                    {
                        player = col[0].gameObject;
                        OnPlayerFound?.Invoke(player);
                        FoundPlayer = true;
                    }
                    
                }
                // else
                // {
                //     FoundPlayer = false;
                // }

        }
    }

   

    private void OnDrawGizmos()
    {
        Gizmos.color = new Vector4(1, 0, 0, 0.7f);
        Gizmos.DrawSphere(this.transform.position + this.transform.forward * sphereOffset, VisionDistance);
    }
    private void OnEnable()
    {
        SC_LightManager.OnSwitchOff += BeginToSearchPlayer;
    }
    
    private void OnDisable()
    {
        SC_LightManager.OnSwitchOff -= BeginToSearchPlayer;
        SC_LightManager.OnSwitchOn += StopToSearchPlayer;
        //canSearch = false;
    }
    private void StopToSearchPlayer()
    {
        canSearch = false;
    }

    private void BeginToSearchPlayer()
    {
        canSearch = true;
    }
}


