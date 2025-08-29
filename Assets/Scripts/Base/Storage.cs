using System;
using UnityEngine;

public class Storage : MonoBehaviour
{
    private int _mineralCount = 0;

    public event Action<int> MineralCountChanged;

    private void OnTriggerEnter(Collider other)
    {
        if ( other.TryGetComponent(out Mineral mineral))
        {
            _mineralCount++;
            MineralCountChanged?.Invoke(_mineralCount);
            mineral.SendMessageAboutCollectionComplete();
        }
    }
}
