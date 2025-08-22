public class CollectionTransition : Transition
{
    public override void Init(TaskData data)
    {
        base.Init(data);

        NeedTransit = !TaskData.IsCollectionCompleted;
    }
}
