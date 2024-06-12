using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private LinkedObject _linkedObject;

    private bool _isPressed;

    private void Action()
    {
        // Проигрываем анимации нажатия
        _linkedObject.Action();
    }

    private void OnCollisionEnter(Collision collision)  // Временное решение
    {
        if(_isPressed) return;
        if (collision.gameObject.GetComponent<PlayerMove>())
        {
            _isPressed = true;
            Action();
        }
    }
}
