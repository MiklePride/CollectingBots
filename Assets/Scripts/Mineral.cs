using System;
using UnityEngine;

public class Mineral : MonoBehaviour
{
    public event Action<Mineral> Collected;

    public Transform SpawnPoint {  get; private set; }

    private void OnDisable() => SpawnPoint = null;

    public void SetSpawnPoint(Transform spawnPoint) => SpawnPoint = spawnPoint;

    public void SendMessageAboutCollectionComplete() => Collected?.Invoke(this);
}
