using System;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected TaskData TaskData;

    public IState TargetState => _targetState;

    public bool NeedTransit { get; protected set; }

    private void OnDisable()
    {
        NeedTransit = false;
        TaskData = null;
    }

    private void Awake()
    {
        enabled = false;
    }

    public virtual void Init(TaskData data)
    {
        TaskData = data;
    }
}
