using TMPro;
using UnityEngine;

public class UIResourceShower : MonoBehaviour
{
    [SerializeField] private Storage _storage;
    [SerializeField] private TMP_Text _mineralCountText;

    private void Start()
    {
        _mineralCountText.text = "0";
        _storage.MineralCountChanged += OnCountChenge;
    }

    private void OnDestroy()
    {
        _storage.MineralCountChanged -= OnCountChenge;
    }

    private void OnCountChenge(int count)
    {
        _mineralCountText.text = count.ToString();
    }
}
