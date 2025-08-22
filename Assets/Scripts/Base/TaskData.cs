using UnityEngine;

public class TaskData
{
    private Mineral _targetCollection;
    private Transform _Storage;
    private Transform _idlePoint;

    public Mineral TargetCollection => _targetCollection;
    public Transform Storage => _Storage;
    public Transform IdlePoint => _idlePoint;

    public bool IsCollectionCompleted { get; private set; }

    public bool IsTaskCompleted => TargetCollection == null;

    public TaskData(Mineral target, Transform storage, Transform idlePoint)
    {
        _targetCollection = target;
        _Storage = storage;
        _idlePoint = idlePoint;
        IsCollectionCompleted = false;

        if (_targetCollection == null)
            IsCollectionCompleted = true;
    }

    public void CompleteTask()
    {
        _targetCollection = null;
    }

    public void CompleteCollection()
    {
        IsCollectionCompleted = true;
    }
}
