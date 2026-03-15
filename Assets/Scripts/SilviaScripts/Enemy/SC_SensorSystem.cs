using System;
using UnityEngine;

public class SC_SensorSystem : MonoBehaviour
{
    //Se enciende cuando está investigando?
    //Comprobar donde está el jugador
    //Lograr verle con 33 grados de vision
    //Comprobar que no hayan objetos que impidan la visión
    //Pasar al estado de persecución

    [field: SerializeField] public float SensorAngle { get; private set; } //si son 33 grados, el sensor va a ser de 16.5 grados a cada lado del enemigo
    [field: SerializeField] public float VisionAngle { get; private set; }
    [SerializeField] private LayerMask isAPlayer;
    [SerializeField] private LayerMask isAnObstacle;
    private GameObject player; //el player que va a ser enviado a otros scripts
    
    //se necesita enviar un evento con una referencia al gameobjet
    public static System.Action<GameObject>OnPlayerFound;
    private void FixedUpdate()
    {
        Collider[] col = Physics.OverlapSphere(this.transform.position, VisionAngle, isAPlayer);
        if (col.Length < 0&&col[0].gameObject.CompareTag("Player"))
        {
            //ver si el jugador está dentro del rango de visión
            Vector3 directionToTarget = col[0].transform.position - this.transform.position;

            if (Vector3.Angle(this.transform.forward, directionToTarget) <=
                SensorAngle / 2) //tiene que estar en su visión que se
                //divide entre dos porque son 16.5 para cada lado
            {
                //comprobar que no haya objetos
                //para eso deebes saber que es un obstáculo
                if (!Physics.Raycast(this.transform.position, directionToTarget, out RaycastHit hit, VisionAngle,
                        isAnObstacle))
                {
                    player = hit.collider.gameObject; 
                    OnPlayerFound?.Invoke(player);
                }
            }
        }
    }
    public Vector2 DirFromAngle(float angle)
    {
        return new Vector3(MathF.Sin(angle)* Mathf.Rad2Deg,0.00f,MathF.Cos(angle)* Mathf.Rad2Deg);
    }
}
