using System;
using UnityEngine;

public class SC_PerceptionSystem : MonoBehaviour
{
    public bool canHearPlayer;
    public bool canSeePlayer;

    private void OnEnable()
    {
        SC_SensorSystem.OnPlayerFound += OnPlayerFound;
        SC_ChaseAttackState.OnPlayerLost += OnPlayerLost;
        SC_ListeningSystem.CanSeeTarget += CanSeeTarget;
        SC_ListeningSystem.CantSeeTarget += CantSeeTarget;
    }

    private void CantSeeTarget()
    {
        canSeePlayer = false;
    }

    private void CanSeeTarget()
    {
        canSeePlayer = true;
    }

    private void OnPlayerLost(Vector3 obj)
    {
        canSeePlayer = false;
    }

    private void OnPlayerFound(GameObject obj)
    {
        canSeePlayer = true;
    }

    private void OnDisable()
    {
        throw new NotImplementedException();
    }
}
