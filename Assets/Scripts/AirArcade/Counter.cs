using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private int _planeLeft;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _boss;
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _spawner;


    private void Start()
    {
        _boss = FindObjectOfType<BossAI>().gameObject;
        _boss.SetActive(false);
    }

    public void CheckWin()
    {
        _planeLeft--;
        _text.text = _planeLeft.ToString();
        if (_planeLeft <= 0)
        {
            _spawner = FindObjectOfType<Spawner>().gameObject;
            if (_spawner != null)
            {
                _spawner.SetActive(false);
            }
            
            _boss.SetActive(true);
        }
    }

    public void Win()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        _winWindow.SetActive(true);
    }
    
    public void Restart()
    {
        _boss = FindObjectOfType<BossAI>().gameObject;
        _boss.SetActive(false);
        _planeLeft = 60;
        _text.text = _planeLeft.ToString();
    }
}
