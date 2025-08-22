using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BaseScanner))]
public class Base : MonoBehaviour
{
    [SerializeField] private List<Bot> _bots = new();
    [SerializeField] private Transform _storage;
    [SerializeField] private Transform _idlePoint;

    private BaseScanner _scanner;

    private void Awake()
    {
        _scanner = GetComponent<BaseScanner>();
    }

    private void Start()
    {
        foreach (Bot bot in _bots)
        {
            if (_scanner.TryGetNearestMineral(out Mineral mineral))
            {
                AssignBotToCollection(bot, mineral);
            }
        }
    }

    private void Update()
    {
        foreach(Bot bot in _bots)
        {
            if (bot.IsIdle)
            {
                if (_scanner.TryGetNearestMineral(out Mineral mineral))
                {
                    AssignBotToCollection(bot, mineral);
                }
            }
        } 
    }

    private void AssignBotToCollection(Bot bot, Mineral mineral) => bot.AssignTask(new TaskData(mineral, _storage, _idlePoint));
}
