using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum GameState
    {
        StartUI,    // 게임 시작 전
        Playing,    // 플레이 중
        Paused,     // 일시 정지
        GameOver    // 실패 또는 성공 종료
    }
    public GameState currentState;
    public Text scoreText;
    private float score = 0;
    public GameObject sabersRight;           // 좌/우 컨트롤러
    public GameObject sabersLeft;           // 좌/우 컨트롤러
    public GameObject spawner;
    public GameObject gameplayUI;       // 플레이 중 UI
    public AudioSource musicPlayer;
    public Slider healthSlider;   // ← Inspector에서 연결
    private float health = 100f;
    private float currentHealth;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateScoreText();
        SetStart();

        currentHealth = health;              // ? 이 줄을 반드시 추가
        healthSlider.value = currentHealth;
        
        if (SelectedSongHolder.selectedSong != null)
        {
            Debug.Log("선택된 곡: " + SelectedSongHolder.selectedSong.title);
            Debug.Log("클립: " + SelectedSongHolder.selectedSong.previewClip);

            musicPlayer.clip = SelectedSongHolder.selectedSong.previewClip;
            musicPlayer.Play();
        }

    }

    // 점수 관련 함수.
    public void AddScore(float amount)
    {
        score += amount;
        UpdateScoreText();
    }

    public void MissCube()
    {
        score -= 1;
        UpdateScoreText();

        
        DecreaseHealth(5f);
        UpdateHealthUI();
        
    }
    void DecreaseHealth(float amount)
    {
        currentHealth = currentHealth - amount;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0f)
        {
            Debug.Log("Game Over!");
            SetGameState(GameState.GameOver);
        }
    }
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    // 게임 상태 관리
    public void SetGameState(GameState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case GameState.StartUI:

                gameplayUI.SetActive(false);
                sabersRight.SetActive(false);
                sabersLeft.SetActive(false);

                Time.timeScale = 0f; // 일시정지
                break;

            case GameState.Playing:

                gameplayUI.SetActive(true);
                sabersRight.SetActive(true);
                sabersLeft.SetActive(true);

                score = 0;
                UpdateScoreText();
                Time.timeScale = 1f;
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                break;

            case GameState.GameOver:
                SceneManager.LoadScene("Start");
                Time.timeScale = 0f;
                break;
        }
    }
    public void SetStart()
    {
        SetGameState(GameState.Playing);
    }


    private void UpdateHealthUI()
    {
        if (healthSlider != null)
            healthSlider.value = health;
    }
}
