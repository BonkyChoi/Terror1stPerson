using UnityEngine;

public abstract class SC_State : MonoBehaviour
{
    public abstract void EnterState(SC_FSMController controller);

    public abstract void UpdateState();

    public abstract void ExitState();
}
