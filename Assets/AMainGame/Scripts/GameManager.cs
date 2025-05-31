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
    public Text comboText;
    private int comboCount = 0;
    private float score = 0;
    public GameObject sabersRight;
    public GameObject sabersLeft;
    public GameObject spawner;
    public GameObject gameplayUI;
    public AudioSource musicPlayer;
    public HealthBarController healthBar;
    public GameObject pauseMenuUI;
    public Slider volumeSlider;

    private float health = 100f;
    public AudioSource sfxPlayer;
    public AudioClip hitSound;
    

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {

        SetStart();

        if (healthBar != null)
            healthBar.SetMaxHealth(health);

        if (SelectedSongHolder.selectedSong != null)
        {
            Debug.Log("���õ� ��: " + SelectedSongHolder.selectedSong.title);
            Debug.Log("Ŭ��: " + SelectedSongHolder.selectedSong.previewClip);

            musicPlayer.clip = SelectedSongHolder.selectedSong.previewClip;
            musicPlayer.Play();
        }

    }
    void UpdateComboText()
    {
        if (comboCount > 0)
        {
            comboText.text = "Combo : " + comboCount;
            comboText.gameObject.SetActive(true);
        }
        else
        {
            comboText.text = "Combo : 0";

        }
    }
    public void RegisterHit()
    {
        comboCount++;
        UpdateComboText();
    }

    public void ResetCombo()
    {
        comboCount = 0;
        UpdateComboText();
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
        ResetCombo();
        DecreaseHealth(5f);

    }
    void DecreaseHealth(float amount)
    {
        health = Mathf.Clamp(health - amount, 0f, 100f);
        healthBar.SetHealth(health);

        if (health <= 0f)
        {
            SetGameState(GameState.GameOver);
        }
    }
    public void IncreaseHealth(float amount)
    {
        if (health <= 100)
        {
            health = Mathf.Clamp(health + amount, 0f, 100f);
            healthBar.SetHealth(health);
            Debug.Log("health UP" + amount);
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
                // sabersRight.SetActive(false);
                // sabersLeft.SetActive(false);

                Time.timeScale = 0f; // �Ͻ�����
                break;

            case GameState.Playing:

                gameplayUI.SetActive(true);
                sabersRight.SetActive(true);
                sabersLeft.SetActive(true);
                pauseMenuUI.SetActive(false);
                score = 0;
                health = 100f;
                UpdateScoreText();
                Time.timeScale = 1f;
                if (!musicPlayer.isPlaying)
                    musicPlayer.UnPause();
                break;

            case GameState.Paused:
                pauseMenuUI.SetActive(true);
                Time.timeScale = 0f;
                sabersRight.SetActive(true);
                sabersLeft.SetActive(true);
                musicPlayer.Pause();
                if (sfxPlayer != null && hitSound != null)
                {
                    sfxPlayer.PlayOneShot(hitSound);
                }
                break;

            case GameState.GameOver:
                SceneManager.LoadScene("RealGameOver");
                Time.timeScale = 1f;
                break;
        }
    }
    public void SetStart()
    {
        SetGameState(GameState.Playing);
    }
    public void TogglePause()
    {

        if (currentState == GameState.Playing)
        {
            SetGameState(GameState.Paused);
            pauseMenuUI.SetActive(true);

            // 슬라이더 초기값을 현재 볼륨에 맞춤
            if (volumeSlider != null)
                volumeSlider.value = musicPlayer.volume * 100f;
        }
        else if (currentState == GameState.Paused)
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        SetGameState(GameState.Playing);
        pauseMenuUI.SetActive(false);
    }
    public void OnVolumeChanged(float value)
    {
        musicPlayer.volume = value / 100f;
    }

}
