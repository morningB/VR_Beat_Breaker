using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
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
    public GameState currentState = GameState.StartUI;
    public Text scoreText;
    private float score = 0;
    public GameObject sabersRight;           // ��/�� ��Ʈ�ѷ�
    public GameObject sabersLeft;           // ��/�� ��Ʈ�ѷ�
    public GameObject spawner;
    public GameObject gameplayUI;       // �÷��� �� UI
    public GameObject startUI;          // ����ȭ�� UI

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateScoreText();
        SetGameState(GameState.StartUI);
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
                startUI.SetActive(true);
                gameplayUI.SetActive(false);
                sabersRight.SetActive(false);
                sabersLeft.SetActive(false);
                spawner.SetActive(false);
                foreach (Transform child in spawner.transform)
                {
                    child.gameObject.SetActive(false);
                }
                Time.timeScale = 0f; // �Ͻ�����
                break;

            case GameState.Playing:
                startUI.SetActive(false);
                gameplayUI.SetActive(true);
                sabersRight.SetActive(true);
                sabersLeft.SetActive(true);
                spawner.GetComponent<Spawner>().enabled = true;
                foreach (Transform child in spawner.transform)
                {
                    child.gameObject.SetActive(true);
                }
                score = 0;
                UpdateScoreText();
                Time.timeScale = 1f;
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                break;

            case GameState.GameOver:
                Time.timeScale = 0f;
                break;
        }
    }
    public void SetStart()
    {
        SetGameState(GameState.Playing);
    }
}
