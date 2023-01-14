using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip attackAudioClip;
    public AudioClip bombAudioClip;
    public AudioClip gameOverAudioClip;
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public Button restartButton;
    public bool isGameActive;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public int lives = 3;
    public Slider volumeSlider;
    private int score;
    private float spawnRate = 1.0f;
    private bool isPaused = false;


    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives " + lives;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volumeSlider.value;
    }

    private void Update()
    {
        PauseGame();
    }

    void OnEnable()
    {
        volumeSlider.onValueChanged.AddListener(delegate { changeVolume(volumeSlider.value); });
    }

    public void PauseGame()
    {
        if (isGameActive && Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0f;
                pauseScreen.gameObject.SetActive(isPaused);
                audioSource.Pause();
            }
            else
            {
                Time.timeScale = 1;
                pauseScreen.gameObject.SetActive(isPaused);
                audioSource.Play();
            }
        }
    }

    void changeVolume(float sliderValue)
    {
        audioSource.volume = sliderValue;
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }

    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives()
    {
        lives -= 1;
        livesText.text = "Lives: " + lives;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
        audioSource.Stop();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);

        titleScreen.gameObject.SetActive(false);
    }
}
