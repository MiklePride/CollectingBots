using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private State _defaultState;

    private TaskData _taskData;
    private IState _currentState;

    public bool IsIdleState
    {
        get
        {
            if (_taskData == null)
                return false;

            return _taskData.IsTaskCompleted;
        }
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        IState nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    private void Reset()
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = _defaultState;

        if (_currentState != null)
            _currentState.Enter(_taskData);
    }

    private void Transit(IState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_taskData);
    }

    public void AssignTask(TaskData taskData)
    {
        _taskData = taskData;

        Reset();
    }
}
