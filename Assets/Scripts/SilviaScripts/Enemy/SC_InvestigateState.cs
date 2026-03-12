using UnityEngine;

public class SC_InvestigateState : SC_State
{
    //Aparece cuando pierde de vista al jugador mientras esta en chase state, o cuando escucha un sonido sospechoso mientras esta en patrol state
    //Debe buscar el origen del sonido, si encuentra algo sospechoso, cambiar a chase state, sino volver a patrol state

    public override void EnterState(SC_FSMController controller)
    {
        // Code to execute when entering the state
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        // Code to execute when exiting the state
    }
}
