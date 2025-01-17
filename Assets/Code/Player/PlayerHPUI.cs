using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHPUI : MonoBehaviour
{
    [SerializeField] private int _maxHartCount;
    [SerializeField] private Image _myPanel;
    [SerializeField] private Image _hart;

    private int _currentHartCount;

    private List<Image> _harts = new List<Image>();

    private void Awake()
    {
        _harts = GetComponentsInChildren<Image>().ToList();

        if(_harts.Contains(_myPanel))
            _harts.Remove(_myPanel);

        _currentHartCount = _maxHartCount;
    }

    private void Update()
    {
        if (Keyboard.current.qKey.wasReleasedThisFrame)
        {
            Heal();
        }
    }

    public void DescountHart()
    {
        if (_harts.Count == 0) return;

        Destroy(_harts[_harts.Count - 1].gameObject);
        _harts.Remove(_harts[_harts.Count - 1]);
        _currentHartCount--;
    }

    public void Heal()
    {
        if (_currentHartCount + 1 > _maxHartCount) return;

        Image hart = Instantiate(_hart,transform);
        _harts.Add(hart);
        _currentHartCount++;
    }
}
