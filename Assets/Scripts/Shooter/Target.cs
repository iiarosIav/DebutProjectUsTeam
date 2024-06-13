using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Target : MonoBehaviour
{
    [SerializeField] private float _xPos1;
    [SerializeField] private float _xPos2;
    [SerializeField] private float _time;
    private ShooterPlayer _player;

    private int _health = 2;
    
    private void Start()
    {
        _player = FindObjectOfType<ShooterPlayer>();
        StartCoroutine(GoToPos(transform.localPosition.x, _xPos2));
    }

    private IEnumerator GoToPos(float firstPos, float secondPos)
    {
        for (float t = 0; t < 1f; t += (Time.deltaTime / _time))
        {
            transform.localPosition = new Vector3(Mathf.Lerp(firstPos, secondPos, t), transform.localPosition.y,
                transform.localPosition.z);
            yield return null;
        }
        transform.localPosition = new Vector3(secondPos, transform.localPosition.y, transform.localPosition.z);
        StartCoroutine(GoToPos(secondPos, firstPos));

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Bullet>())
        {
            _health--;
            if (_health <= 0)
            {
                Die();
            }
        }
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Bullet>())
        {
            Die();
        }
    }

    private void Die()
    {
        _player.TargetCoounter += 1;
        _player.TargetCheck();
        Destroy(gameObject);
    }
}
