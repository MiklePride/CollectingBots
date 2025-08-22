using System.Collections.Generic;
using UnityEngine;

public class BaseScanner : MonoBehaviour
{
    [SerializeField] private float _radius = 100f;
    [SerializeField] private LayerMask _mineralMask;

    private List<Mineral> _detectedMinerals = new();

    private void ScanForMinerals()
    {
        if (_detectedMinerals.Count > 0)
            _detectedMinerals.Clear();

        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _mineralMask);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<Mineral>(out Mineral mineral) && !mineral.IsOccupied)
            {
                _detectedMinerals.Add(mineral);
            }
        }
    }

    public bool TryGetNearestMineral(out Mineral mineral)
    {
        mineral = null;

        if (_detectedMinerals.Count == 0)
            ScanForMinerals();

        if (_detectedMinerals.Count != 0)
        {
            foreach (var resource in _detectedMinerals)
            {
                if (mineral == null)
                {
                    if (!resource.IsOccupied)
                    {
                        mineral = resource;
                    }

                    continue;
                }

                if (!resource.IsOccupied && Vector3.Distance(resource.gameObject.transform.position, transform.position) < 
                    Vector3.Distance(mineral.gameObject.transform.position, transform.position))
                {
                    mineral = resource;
                }
            }

        }

        if (mineral != null)
        {
            mineral.Occupy();
            _detectedMinerals.Remove(mineral);
        }

        return mineral != null;
    }
}
