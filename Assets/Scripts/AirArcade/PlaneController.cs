using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private float _xMin;
    [SerializeField] private float _xMax;

    [SerializeField] private float _zMin;
    [SerializeField] private float _zMax;

    [SerializeField] private Bullet _bullet;
    [SerializeField] private GameObject _fX;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private float _bulletSpeed = 20f;
    [SerializeField] private float _shotPeriod = 0.2f;

    private float _timer;


    private void Update()
    {
        transform.position += new Vector3(_speed * Time.deltaTime * Input.GetAxis("Horizontal"), 0, _speed * Time.deltaTime * Input.GetAxis("Vertical"));

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _xMin, _xMax), transform.position.y, Mathf.Clamp(transform.position.z, _zMin, _zMax));

        _timer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (_timer >= _shotPeriod)
            {
                Fire();
                _timer = 0;
            }
        }
    }

    private void Fire()
    {
        Bullet newBullet = Instantiate(_bullet, _bulletSpawn.position, _bulletSpawn.rotation);
        newBullet.GetComponent<Rigidbody>().velocity = _bulletSpawn.forward * _bulletSpeed;
        Instantiate(_fX, _bulletSpawn.transform.position + new Vector3(0, 0.2f, 0), transform.rotation);
    }

}