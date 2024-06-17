using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMiniGame : MonoBehaviour
{
    public int SceneNum;
    public void StartGame()
    {
        SceneManager.LoadScene(SceneNum);
    }
}
