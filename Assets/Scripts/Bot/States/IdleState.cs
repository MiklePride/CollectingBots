public class IdleState : State
{
    private void OnEnable()
    {
        Mover.MoveTo(TaskData.IdlePoint);
    }
}
