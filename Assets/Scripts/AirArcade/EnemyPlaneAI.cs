using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlaneAI : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _shotPeriod;
    [SerializeField] private GameObject _fX;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private GameObject _fXShot;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private float _bulletSpeed = 20f;
    [SerializeField] private GameObject _smoke;

    private bool _canShoot = true;
    private float _timer;

    Counter counter;

    void Start()
    {
        counter = FindObjectOfType<Counter>();
        Invoke("Destr", 10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
        _timer += Time.deltaTime;
        if (_timer >= _shotPeriod && _canShoot)
        {
            Bullet newBullet = Instantiate(_bullet, _bulletSpawn.position, _bulletSpawn.rotation);
            newBullet.GetComponent<Rigidbody>().velocity = _bulletSpawn.forward * _bulletSpeed;
            Instantiate(_fXShot, _bulletSpawn.transform.position + new Vector3(0, 0.2f, 0), transform.rotation);
            _timer = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
        {
            Die();
            Destroy(collision.gameObject);
        }
    }

    public void Die()
    {
        Instantiate(_fX, transform.position, transform.rotation);
        transform.Rotate(UnityEngine.Random.Range(-20f, 20f), UnityEngine.Random.Range(-45f, 45f),
            UnityEngine.Random.Range(-80f, 80f));
        _smoke.SetActive(true);
        _canShoot = false;
        _speed *= 2;
        counter.CheckWin();
    }

    private void Destr()
    {
        Destroy(gameObject);
    }
}