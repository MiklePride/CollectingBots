using System;
using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _distanceOffset = 0.3f;

    private Coroutine _coroutine;

    public event Action MovedCompleted;

    public void MoveTo(Transform target)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(GoToTarget(target));
    }

    private IEnumerator GoToTarget(Transform targetPoint)
    {
        while (Vector3.Distance(transform.position, targetPoint.position) > _distanceOffset)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, _speed * Time.deltaTime);

            yield return null;
        }

        MovedCompleted?.Invoke();
    }
}
