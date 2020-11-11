using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    public void GameOver()
    {
        _isGameOver = true;
    }

    private void Update()
    {
        if(_isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            //Load Main Menu scene
            SceneManager.LoadScene(1);
        }
    }
}
