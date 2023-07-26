using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //Text
using UnityEngine.SceneManagement; //Reload Scene
using UnityEngine.UI; //Button

public class GameManager : MonoBehaviour
{
    public GameObject[] targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button pauseButton;
    public GameObject titleScreen;
    public ParticleSystem explosionParticle;
    private float spawnRate = 1.0f;
    private int score;
    public bool isGameActive;
    public int lives;
    private bool paused;
    public Color pausedColor;
    public Color normalColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        titleScreen.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        pauseButton.onClick.AddListener(OnPauseButtonClicked);
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        lives = 3;
        UpdateLives(0);
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnTarget() 
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Length);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToSubtract)
    {
        if (isGameActive)
        {
            lives -= livesToSubtract;
            livesText.text = "Lives: " + lives;
            if (lives == 0)
            {
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnPauseButtonClicked()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1;
        }
        else
        {
            paused = true;
            Time.timeScale = 0;
        }
        ChangeButtonColor();
    }

    private void ChangeButtonColor()
    {
        Image buttonImage = pauseButton.GetComponent<Image>();
        if (paused)
        {
            buttonImage.color = pausedColor; 
            Debug.Log("ChangeButtonColor " + pausedColor);
        }
        else
        {
            buttonImage.color = normalColor;
            Debug.Log("ChangeButtonColor " + normalColor);
        }
    }
}
