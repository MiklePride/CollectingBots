using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MineralSpawner : MonoBehaviour
{
    [SerializeField] private Mineral _prefab;
    [SerializeField] private List<Transform> _freeSpawnPoints;
    [SerializeField] private float _spawnDelay = 3;

    private List<Transform> _busySpawnPoints = new();
    private List<Mineral> _mineralPool = new();

    private float _spawnTimer = 0;

    private void Awake()
    {
        foreach (var spawnPoint in _freeSpawnPoints)
        {
            Mineral mineral = Get();
            mineral.SetSpawnPoint(spawnPoint);
            mineral.transform.position = spawnPoint.position;
        }

        _busySpawnPoints.AddRange(_freeSpawnPoints);
        _freeSpawnPoints.Clear();
    }

    private void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _spawnDelay)
        {
            TrySpawn();
            _spawnTimer = 0;
        }
    }

    private void TrySpawn()
    {
        if (_freeSpawnPoints.Count == 0) return;

        Mineral mineral = Get();
        mineral.SetSpawnPoint(_freeSpawnPoints[0]);
        mineral.transform.position = mineral.SpawnPoint.position;

        _freeSpawnPoints.Remove(mineral.SpawnPoint);
        _busySpawnPoints.Add(mineral.SpawnPoint);
    }

    private Mineral Get()
    {
        Mineral mineral = _mineralPool.FirstOrDefault(mineral => !mineral.isActiveAndEnabled);

        if (mineral == null)
        {
            mineral = Instantiate(_prefab);
            _mineralPool.Add(mineral);
        }

        mineral.gameObject.SetActive(true);
        mineral.Collected += OnRelease;

        return mineral;
    }

    private void OnRelease(Mineral mineral)
    {
        mineral.transform.position = transform.position;

        _busySpawnPoints.Remove(mineral.SpawnPoint);
        _freeSpawnPoints.Add(mineral.SpawnPoint);

        mineral.Collected -= OnRelease;
        mineral.gameObject.SetActive(false);
    }
}
