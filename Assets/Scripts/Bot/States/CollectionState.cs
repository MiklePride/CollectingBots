using System.Collections;
using UnityEngine;

public class CollectionState : State
{
    [SerializeField] private float _collectionTime = 3f;
    [SerializeField] private Transform _resourceContainer;
    
    private Coroutine _coroutine;

    private void OnEnable()
    {
        Mover.MoveTo(TaskData.TargetCollection.transform);
        Mover.MovedCompleted += OnCollect;
    }

    private IEnumerator StartTimer(float endTime)
    {
        yield return new WaitForSeconds(endTime);

        TaskData.TargetCollection.transform.position = _resourceContainer.transform.position;
        TaskData.TargetCollection.transform.SetParent(_resourceContainer);

        Mover.MovedCompleted -= OnCollect;
        TaskData.CompleteCollection();
    }

    private void OnCollect()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(StartTimer(_collectionTime));
    }
}
