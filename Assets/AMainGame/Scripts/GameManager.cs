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
        StartUI,    // ���� ���� ��
        Playing,    // �÷��� ��
        Paused,     // �Ͻ� ����
        GameOver    // ���� �Ǵ� ���� ����
    }
    public GameState currentState;
    public Text scoreText;
    private float score = 0;
    public GameObject sabersRight;           // ��/�� ��Ʈ�ѷ�
    public GameObject sabersLeft;           // ��/�� ��Ʈ�ѷ�
    public GameObject spawner;
    public GameObject gameplayUI;       // �÷��� �� UI
    public AudioSource musicPlayer;
    public Slider healthSlider;   // �� Inspector���� ����
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

        currentHealth = health;              // ? �� ���� �ݵ�� �߰�
        healthSlider.value = currentHealth;
        
        if (SelectedSongHolder.selectedSong != null)
        {
            Debug.Log("���õ� ��: " + SelectedSongHolder.selectedSong.title);
            Debug.Log("Ŭ��: " + SelectedSongHolder.selectedSong.previewClip);

            musicPlayer.clip = SelectedSongHolder.selectedSong.previewClip;
            musicPlayer.Play();
        }

    }

    // ���� ���� �Լ�.
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

    // ���� ���� ����
    public void SetGameState(GameState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case GameState.StartUI:

                gameplayUI.SetActive(false);
                sabersRight.SetActive(false);
                sabersLeft.SetActive(false);

                Time.timeScale = 0f; // �Ͻ�����
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
