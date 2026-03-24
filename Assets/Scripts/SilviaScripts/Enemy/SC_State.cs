using UnityEngine;

public abstract class SC_State : MonoBehaviour
{
    public abstract void OnEnterState(SC_FSMController controller);

    public abstract void OnUpdateState();

    public abstract void OnExitState();
}
