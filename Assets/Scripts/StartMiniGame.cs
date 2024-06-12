using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMiniGame : MonoBehaviour
{
    [SerializeField] private int _sceneNum;

    public void StartGame()
    {
        SceneManager.LoadScene(_sceneNum);
    }
}
