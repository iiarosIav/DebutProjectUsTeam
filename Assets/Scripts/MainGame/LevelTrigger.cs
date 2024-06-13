using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    // [SerializeField] private bool _startGame;
    public int Level;

    private void OnTriggerEnter(Collider other)
    {
        // if (_startGame && Level == 1)
        // {
        //     FindObjectOfType<StartMiniGame>().StartGame(1);
        // }
    }
}
