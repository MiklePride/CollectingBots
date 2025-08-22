using UnityEngine;

[RequireComponent (typeof(StateMachine))]
public class Bot : MonoBehaviour
{
    private StateMachine _stateMachine;

    public bool IsIdle
    {
        get
        {
            return !_stateMachine.IsIdleState;
        }
    }

    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
    }

    public void AssignTask(TaskData taskData)
    {
        _stateMachine.AssignTask(taskData);
    }
}
