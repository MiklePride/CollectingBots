using System.Collections.Generic;
using UnityEngine;

public class BaseScanner : MonoBehaviour
{
    [SerializeField] private float _radius = 100f;
    [SerializeField] private LayerMask _mineralMask;

    public List<Mineral> ScanForMinerals()
    {
        var detectedMinerals = new List<Mineral>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _mineralMask);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Mineral mineral))
            {
                detectedMinerals.Add(mineral);
            }
        }

        return detectedMinerals;
    }
}
