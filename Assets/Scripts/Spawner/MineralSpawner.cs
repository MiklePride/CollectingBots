using System.Collections.Generic;
using UnityEngine;

public class MineralSpawner : MonoBehaviour
{
    [SerializeField] private Mineral _prefab;
    [SerializeField] private float _spawnDelay = 3;
    [SerializeField] private List<SpawnPoint> _spawnPoints;

    private float _spawnTimer = 0;

    private void Awake()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            Instantiate(_prefab.gameObject, spawnPoint.transform.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _spawnDelay)
            TrySpawn();
    }

    private void TrySpawn()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            if (spawnPoint.IsFree)
            {
                Instantiate(_prefab.gameObject, spawnPoint.transform.position, Quaternion.identity);
                _spawnTimer = 0f;

                break;
            }

        }
    }
}
