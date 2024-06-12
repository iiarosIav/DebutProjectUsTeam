using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private float _bulletSpeed = 20f;
    [SerializeField] private float _shotPeriod = 0.2f;
    [SerializeField] private Transform _camera;

    private float _posX;
    private Coroutine _moveCoroutine;

    private float _timer;

    // [SerializeField] private Animator _animator;
    // [SerializeField] private AudioSource _shotAudio;

    private void Start()
    {
        _posX = transform.localPosition.x;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (_timer >= _shotPeriod)
            {
                Fire();
                _timer = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
                _moveCoroutine = StartCoroutine(GoToPos(transform.localPosition.x, 0));
            }
            else
            {
                _moveCoroutine = StartCoroutine(GoToPos(_posX, 0));
            }
            
            // transform.position = new Vector3(_camera.position.x, transform.position.y, transform.position.z);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
                _moveCoroutine = StartCoroutine(GoToPos(transform.localPosition.x, _posX));
            }
            else
            {
                _moveCoroutine = StartCoroutine(GoToPos(0, _posX));
            }
            
            // transform.position = new Vector3(_posX, transform.position.y, transform.position.z);
        }
    }

    private void Fire()
    {
        Bullet newBullet = Instantiate(_bullet, _bulletSpawn.position, _bulletSpawn.rotation);
        newBullet.GetComponent<Rigidbody>().velocity = _bulletSpawn.forward * _bulletSpeed;

        // _animator.SetTrigger("Shot");
        // _shotAudio.Play();
    }

    private IEnumerator GoToPos(float firstPos, float secondPos)
    {
        Debug.Log("Started");
        for (float t = 0; t < 1f; t += (Time.deltaTime / 0.3f))
        {
            transform.localPosition = new Vector3(Mathf.Lerp(firstPos, secondPos, t), transform.localPosition.y,
                transform.localPosition.z);
            yield return null;
        }
        transform.localPosition = new Vector3(secondPos, transform.localPosition.y, transform.localPosition.z);
        _moveCoroutine = null;
    }
}