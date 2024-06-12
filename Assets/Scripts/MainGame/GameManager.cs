using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Location[] _locations;
    private PlayerMove _player;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _player = FindObjectOfType<PlayerMove>();
        _locations = FindObjectsOfType<Location>();
        Array.Sort(_locations);
        LoadLocations();
    }

    [ContextMenu("LoadLocations")]
    public void LoadLocations()
    {
        int level = _player.Level - 1;
        for (int i = 0; i < _locations.Length; i++)
        {
            if (i == level) continue;
            if (i == level - 1) _locations[i].gameObject.SetActive(true);
            else if (i == level + 1) _locations[i].gameObject.SetActive(true);
            else _locations[i].gameObject.SetActive(false);
        }
    }
}
