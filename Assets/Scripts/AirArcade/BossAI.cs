using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField] private float _moveTo;
    [SerializeField] private int _heal;

    [SerializeField] private GameObject _laser1;
    [SerializeField] private GameObject _laser2;

    [SerializeField] private float _speed;
    [SerializeField] private float _angleSpeed;

    private void Update()
    {
        if (transform.position.z >= _moveTo)
        {
            transform.position += transform.forward * Time.deltaTime * _speed;
        }
        
        _laser1.transform.Rotate(0, 1, 0);
        _laser1.transform.Rotate(0, -1, 0);
        

    }
}
