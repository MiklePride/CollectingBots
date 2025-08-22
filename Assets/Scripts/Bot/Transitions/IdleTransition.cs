public class IdleTransition : Transition
{
    private void Update()
    {
        NeedTransit = TaskData.IsTaskCompleted;
    }
}
