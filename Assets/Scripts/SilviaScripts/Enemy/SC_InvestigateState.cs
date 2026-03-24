using UnityEngine;

public class SC_InvestigateState : SC_State
{
    //Aparece cuando pierde de vista al jugador mientras esta en chase state, o cuando escucha un sonido sospechoso mientras esta en patrol state
    //Debe buscar el origen del sonido, si encuentra algo sospechoso, cambiar a chase state, sino volver a patrol state

    public override void OnEnterState(SC_FSMController controller)
    {
        // Code to execute when entering the state
    }

    public override void OnUpdateState()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExitState()
    {
        // Code to execute when exiting the state
    }
}
