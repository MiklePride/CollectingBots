using UnityEngine;

public interface IState
{
    public void Enter(TaskData data);

    public void Exit();

    public IState GetNextState();
}
