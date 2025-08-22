using UnityEngine;

public class Mineral : MonoBehaviour
{
    private bool _isOccupied;

    public bool IsOccupied => _isOccupied;

    private void Start()
    {
        _isOccupied = false;
    }

    public void Occupy()
    {
        _isOccupied = true;
    }
}
