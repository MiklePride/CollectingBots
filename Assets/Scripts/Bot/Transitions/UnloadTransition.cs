public class UnloadTransition : Transition
{
    private void Update()
    {
        NeedTransit = TaskData.IsCollectionCompleted;
    }
}
