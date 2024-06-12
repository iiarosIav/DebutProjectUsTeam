using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    private ShooterPlayer _player;
    
    void Start()
    {
        _player = FindObjectOfType<ShooterPlayer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Bullet>())
        {
            _player.TargetCoounter += 1;
            _player.TargetCheck();
            Destroy(gameObject);
        }
    }
}
