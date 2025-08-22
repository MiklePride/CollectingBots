using System.Collections;
using UnityEngine;

public class UnloadState : State
{
    [SerializeField] private float _unloadingTime = 1f;
    [SerializeField] private Transform _resourceContainer;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        Mover.MoveTo(TaskData.Storage);
        Mover.MovedCompleted += OnUnloading;
    }

    private IEnumerator StartTimer(float endTime)
    {
        yield return new WaitForSeconds(endTime);

        _resourceContainer.DetachChildren();
        TaskData.TargetCollection.transform.position = TaskData.Storage.position;

        Mover.MovedCompleted -= OnUnloading;
        TaskData.CompleteTask();
    }

    private void OnUnloading()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(StartTimer(_unloadingTime));
    }
}
