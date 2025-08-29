using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BaseScanner))]
public class Base : MonoBehaviour
{
    [SerializeField] private List<Bot> _bots = new();
    [SerializeField] private Transform _storage;
    [SerializeField] private Transform _idlePoint;
    [SerializeField] private DataBase _dataBase;

    private BaseScanner _scanner;

    private void Awake()
    {
        _scanner = GetComponent<BaseScanner>();
    }

    private void Start()
    {
        if (!_dataBase.HasFreeResource)
            _dataBase.AddNewResources(_scanner.ScanForMinerals());

        foreach (Bot bot in _bots)
        {
            if (_dataBase.HasFreeResource)
            {
                AssignBotToCollection(bot, _dataBase.GetResource());
            }
        }
    }

    private void Update()
    {
        if (!_dataBase.HasFreeResource)
        {
            _dataBase.AddNewResources(_scanner.ScanForMinerals());
        }

        foreach (Bot bot in _bots)
        {
            if (bot.IsIdle)
            {
                if (_dataBase.HasFreeResource)
                {
                    AssignBotToCollection(bot, _dataBase.GetResource());
                }
            }
        } 
    }

    private void AssignBotToCollection(Bot bot, Mineral mineral) => bot.AssignTask(new TaskData(mineral, _storage, _idlePoint));
}
