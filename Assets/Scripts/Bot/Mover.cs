using System;
using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _distanceOffset = 0.3f;

    private Transform _targetPoint;
    private Coroutine _coroutine;

    public event Action MovedCompleted;

    private IEnumerator GoToTarget()
    {
        while (Vector3.Distance(transform.position, _targetPoint.position) > _distanceOffset)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPoint.position, _speed * Time.deltaTime);

            yield return null;
        }

        _targetPoint = null;
        MovedCompleted?.Invoke();
    }

    public void MoveTo(Transform target)
    {
        _targetPoint = target;

        

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(GoToTarget());
    }
}
