using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverPanel;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public GameObject player;
    public GameObject enemies;

    public Camera gameCamera;
    public int score = Variables.score;
    //global variables
    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public bool alive = true;
    // Start is called before the first frame update
    void Start()
    {
        gameStart.Invoke();
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOverText.text = scoreText.text;
        GameOverPanel.SetActive(true);
    }
    public void GameOverScene()
    {
        // stop time
        Time.timeScale = 0.0f;
        // set gameover scene
        GameOver();
    }
    public void ResetGame()
    {
        gameRestart.Invoke();
        // reset score
        Variables.score = 0;
        
    }
    public void IncreaseScore(int increment)
    {
        score += increment;
        SetScore(score);
    }

    public void SetScore(int score)
    {
        scoreChange.Invoke(score);
    }



}
