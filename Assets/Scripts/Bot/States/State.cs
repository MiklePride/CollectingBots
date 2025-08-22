using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public abstract class State : MonoBehaviour, IState
{
    [SerializeField] private List<Transition> transitions;

    protected Mover Mover;
    protected TaskData TaskData;

    private void Awake()
    {
        enabled = false;
        Mover = GetComponent<Mover>();
    }

    public virtual void Enter(TaskData data)
    {
        TaskData = data;

        if (!enabled)
            enabled = true;

        foreach (var transition in transitions)
        {
            transition.enabled = true;
            transition.Init(data);
        }
    }

    public void Exit()
    {
        if (enabled)
        {
            foreach (var transition in transitions)
                transition.enabled = false;

            enabled = false;
        }
    }

    public IState GetNextState()
    {
        foreach (var transition in transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
        }

        return null;
    }
}
