using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    private List<Mineral> _freeResources = new();
    private List<Mineral> _occupiedResources = new();

    public bool HasFreeResource => _freeResources.Count > 0;

    public void AddNewResources(List<Mineral> newResources)
    {
        if (newResources == null || newResources.Count == 0)
            return;

        if (_freeResources.Count == 0 && _occupiedResources.Count == 0)
        {
            _freeResources = newResources;

            return;
        }

        if (_occupiedResources.Count != 0)
        {
            var minerals = newResources.Except(_occupiedResources);
            newResources = minerals.ToList();
        }

        var newFreeResources = _freeResources.Union(newResources).Distinct();
        _freeResources = newFreeResources.ToList();
    }

    public Mineral GetResource()
    {
        Mineral mineral = null;

        if (_freeResources.Count != 0)
        {
            foreach (var resource in _freeResources)
            {
                if (mineral == null)
                {
                    mineral = resource;

                    continue;
                }

                if (Vector3.Distance(resource.gameObject.transform.position, transform.position) <
                    Vector3.Distance(mineral.gameObject.transform.position, transform.position))
                {
                    mineral = resource;
                }
            }

        }

        if (mineral != null)
        {
            _freeResources.Remove(mineral);
            _occupiedResources.Add(mineral);

            mineral.Collected += OnReleaseResource;
        }

        return mineral;
    }

    private void OnReleaseResource(Mineral resource)
    {
        _occupiedResources.Remove(resource);

        resource.Collected -= OnReleaseResource;
    }
}