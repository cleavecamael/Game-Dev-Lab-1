using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverPanel;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public GameObject player;
    public GameObject enemies;
    public GameObject powerBlocks;
    public GameObject coinBlocks;
    public Camera gameCamera;
    //global variables
    
    // Start is called before the first frame update
    void Start()
    {
        
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
        gameCamera.transform.position = new Vector3(0, 0, -10);
        player.GetComponent<PlayerMovement>().Reset();
        foreach (Transform enemy in enemies.transform){

            enemy.gameObject.GetComponent<EnemyMovement>().Reset();
        }
        foreach (Transform powerBlock in powerBlocks.transform)
        {

            powerBlock.gameObject.GetComponent<QuestionScript>().Reset();
        }
        foreach (Transform coinBlock in coinBlocks.transform)
        {

            coinBlock.gameObject.GetComponent<CoinBlockScript>().Reset();
        }

        // reset score
        scoreText.text = "Score: 0";
       
        // reset score
        Variables.score = 0;
        GameOverPanel.SetActive(false);
    }


}
