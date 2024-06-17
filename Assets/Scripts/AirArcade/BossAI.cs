using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class BossAI : MonoBehaviour
{
    [SerializeField] private float _moveTo;
    [SerializeField] private int _heal;

    [SerializeField] private GameObject _laser1;
    [SerializeField] private GameObject _laser2;

    [SerializeField] private GameObject _fX;

    [SerializeField] private float _speed;
    [SerializeField] private float _angleSpeed;

    bool moveRight = true;
    public float distance = 0.4f;
    public int WaitForSecond = 1;

    float koef1 = -1;
    float koef2 = 1;
    Counter counter;

    bool isDead = false;

    void Start()
    {
        counter = FindObjectOfType<Counter>();
        Invoke("Destr", 10f);
    }

    private void Update()
    {
        if (transform.position.z >= _moveTo)
        {
            transform.position += transform.forward * Time.deltaTime * _speed;
        }
        else
        {
            if (moveRight)
            {
                var pos = transform.position;
                pos.x += _speed * Time.deltaTime;
                transform.position = pos;
                if (pos.x > 20f) moveRight = false;
            }
            else
            {
                var pos = transform.position;
                pos.x -= _speed * Time.deltaTime;
                transform.position = pos;
                if (pos.x < -20f) moveRight = true;
            }
        }

        if (_heal <= 0)
        {
            transform.position += transform.up * -1 * Time.deltaTime * _speed;
            transform.Rotate(0, _angleSpeed * Time.deltaTime, 0);
            
            isDead = true;
            if (!isDead)
            {
                counter.CheckWin();

            }
        }
        

    }

    private void OnCollisionEnter(Collision collision)
    {
            if (collision.gameObject.name.Contains("Bullet"))
            {
                Instantiate(_fX, transform.position, transform.rotation);
                
                Destroy(collision.gameObject);
                _heal--;
            }
  
    }
}
