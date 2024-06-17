using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private int _planeLeft;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _spawner;

    public void CheckWin()
    {
        _planeLeft--;
        _text.text = _planeLeft.ToString();
        if (_planeLeft <= 0)
        {
            _spawner.SetActive(false);
            _winWindow.SetActive(true);
        }
    }
}
