using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject Pref1;
    public GameObject Pref2;
    public GameObject Pref3;
    [SerializeField] private float _shootingDelay = 6f;

    private float _currentShootingDelayCount = 0f;

    int col = 20;
        

    void Update()
    {
        _currentShootingDelayCount += Time.deltaTime;
        
        if (_currentShootingDelayCount > _shootingDelay && col > 0)
        {
            TryS();
            
        }
        
    }
    private void TryS()
    {  
        int value = Random.Range(0,300);
        if (value <= 100) {
            Instantiate(Pref1, transform.position, transform.rotation);
        }
        else if (value > 200)
        {
            Instantiate(Pref2, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(Pref3, transform.position, transform.rotation);
        }
        _currentShootingDelayCount = 0;
        col--;
    }
}
