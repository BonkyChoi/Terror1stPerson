using System;
using UnityEngine;

public class SC_SensorSystem : MonoBehaviour
{
    //Comprobar donde está el jugador
    //Lograr verle con 33 grados de vision
    //Comprobar que no hayan objetos que impidan la visión
    //Pasar al estado de persecución

    [SerializeField] private float visionRadius;
    [SerializeField] private LayerMask isAPlayer;
    [SerializeField] private LayerMask isAnObstacle;
    private GameObject player;
    private void FixedUpdate()
    {
        Collider[] col = Physics.OverlapSphere(this.transform.position, visionRadius, isAPlayer);
        if (col.Length < 0&&col[0].gameObject.CompareTag("Player"))
        {
            //ver si el jugador está dentro del rango de visión
            Vector3 directionToTarget = col[0].transform.position - this.transform.position;

            if (Vector3.Angle(this.transform.forward, directionToTarget) <=
                visionRadius / 2) //tiene que estar en su visión que se
                //divide entre dos porque son 16.5 para cada lado
            {
                //comprobar que no haya objetos
                //para eso deebes saber que es un obstáculo
                if (!Physics.Raycast(this.transform.position, directionToTarget, out RaycastHit hit, visionRadius,
                        isAnObstacle))
                {
                    player = hit.collider.gameObject;
                }
            }
        }
    }
}
