using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }
    [SerializeField] private UIHandler _uiHandler;
    public bool isPlaying = true;
    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public int score = 0;

    public void AddDiamondToScore()
    {
        score += SerializedVariables.Instance.scorePerDiamond;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        isPlaying = false;
        _uiHandler.ShowGameOverPanel();
    }



    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
