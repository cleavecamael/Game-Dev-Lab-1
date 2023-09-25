using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject GameOverPanel;
    public TextMeshProUGUI gameOverText;

  
    public AudioSource marioAudio;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Restart()
    {
        scoreText.text = "Score: 0";
        GameOverPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void RestartButtonCallback(int input)
    {
        // reset everything
        gameManager.ResetGame();
        marioAudio.Stop();
    }
    public void SetScore(int score)
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }

}
