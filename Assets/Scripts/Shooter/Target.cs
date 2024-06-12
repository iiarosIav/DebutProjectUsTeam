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
    
    private void Start()
    {
        _player = FindObjectOfType<ShooterPlayer>();
        StartCoroutine(GoToPos(transform.position.x, _xPos2));
    }

    private IEnumerator GoToPos(float firstPos, float secondPos)
    {
        for (float t = 0; t < 1f; t += (Time.deltaTime / _time))
        {
            transform.position = new Vector3(Mathf.Lerp(firstPos, secondPos, t), transform.position.y,
                transform.position.z);
            yield return null;
        }
        transform.position = new Vector3(secondPos, transform.position.y, transform.position.z);
        StartCoroutine(GoToPos(secondPos, firstPos));

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
