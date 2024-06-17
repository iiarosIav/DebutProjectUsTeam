using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlane : MonoBehaviour
{
    [SerializeField] private GameObject _mainGame;
    [SerializeField] private GameObject _miniGame;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMove>())
        {
            _miniGame.SetActive(true);
            _mainGame.SetActive(false);
        }
    }
}
