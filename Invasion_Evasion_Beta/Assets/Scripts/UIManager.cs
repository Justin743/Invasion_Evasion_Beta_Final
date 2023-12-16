using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI difficultyText;

    public GameObject player;

    private PlayerController playerController;
    private SpawnManager spawnManager;

    private int round;
    private int health;
    private int score;

    public Button restartButton;
    public Button StartButton;
    public Button difficultyButton;
    public bool isGameActive = false;

    public Button urlButton;
    //Link to GitHub repository
    private string URL = "https://github.com/Justin743/Invasion_Evasion_Beta_Final";



    // Start is called before the first frame update
    void Start()
    {
        //Imports PlayerController and SpawnManager scripts
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
       

        //sets score to 0 at start 
        score = 0;
        ScoreText.text = "Score : " + score;
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }
    //UpdateHealth Method
    public void UpdateHealth(int newHealth)
    {
        
        health = newHealth;
        healthText.text = "Health : " + health;
    }
    //Updates the round number in the HUD if a player progresses through a wave of enemies
    public void UpdateRoundNumber(int roundNum)
    {
        round = roundNum;
        roundText.text = "Round " + roundNum;
        
    }

    //Updates the score in the HUD when a player destroys an enemy
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        ScoreText.text = "Score : " + score;
    }

    //GameOver triggered when the players health reaches 0
    //Game over text and a restart button are set active
    public void gameOver() { 

        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        player.gameObject.SetActive(false);

    }

    //Loads the scene back to the beginning when restart is clicked
    public void restartGame() { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NormalMode()
    {
        SceneManager.LoadScene("Scene1");
    }
    public void HardMode()
    {
        SceneManager.LoadScene("Scene2");
    }
    //Starts the game when the start game button is clicked
    //The title, start button and github link are set to inactive
    //The player, health text, round text and score text are set to active  
    public void StartGame()
    {
        isGameActive = true;
        titleText.gameObject.SetActive(false);
        difficultyText.gameObject.SetActive(false);
        urlButton.gameObject.SetActive(false);
        StartButton.gameObject.SetActive(false);
        difficultyButton.gameObject.SetActive(false);
        healthText.gameObject.SetActive(true);
        roundText.gameObject.SetActive(true);
        ScoreText.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        

       
    }

    //Loads the github repository webpage when the "GitHub Link" button is pressed
    public void loadURL() 
    {
        Application.OpenURL(URL);
    }
}
