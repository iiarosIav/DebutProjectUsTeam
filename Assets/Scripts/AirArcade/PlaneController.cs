using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField] private int _heal = 5;
    public GameObject LoseScreen;
    public TextMeshProUGUI HealText;

    [SerializeField] private Transform _miniGame;
    [SerializeField] private GameObject _miniGamePrefab;
    [SerializeField] private Counter _counter;

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

        if (_heal <= 0)
        {
            Instantiate(_fX, transform.position + new Vector3(0, 0.2f, 0), transform.rotation);
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            LoseScreen.SetActive(true);
        }
    }

    public void Restart()
    {
        LoseScreen.SetActive(false);
        _heal = 5;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        HealText.text = _heal.ToString();
        var miniGame = Instantiate(_miniGamePrefab, _miniGame.transform.position, 
            _miniGame.transform.rotation).transform;
        miniGame.SetParent(_miniGame.parent);
        Destroy(_miniGame.gameObject);
        _miniGame = miniGame;
        
        _counter.Restart();
    }

    private void Fire()
    {
        Bullet newBullet = Instantiate(_bullet, _bulletSpawn.position, _bulletSpawn.rotation);
        newBullet.GetComponent<Rigidbody>().velocity = _bulletSpawn.forward * _bulletSpeed;
        Instantiate(_fX, _bulletSpawn.transform.position + new Vector3(0, 0.2f, 0), transform.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Enemy"))
        {
            Instantiate(_fX, collision.gameObject.transform.position + new Vector3(0, 0.2f, 0), transform.rotation);
            _heal--;
            HealText.text = _heal.ToString();
            if (collision.gameObject.GetComponent<EnemyPlaneAI>() is EnemyPlaneAI _enemy)
            {
                _enemy.Die();
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.name.Contains("laser") || collision.gameObject.name.Contains("Boss"))
        {
            Instantiate(_fX, collision.gameObject.transform.position + new Vector3(0, 0.2f, 0), transform.rotation);
            _heal--;
            HealText.text = _heal.ToString();
        }
    }

}