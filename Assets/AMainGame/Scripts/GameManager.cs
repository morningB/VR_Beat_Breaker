using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
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
    public GameState currentState = GameState.StartUI;
    public Text scoreText;
    private float score = 0;
    public GameObject sabersRight;           // 좌/우 컨트롤러
    public GameObject sabersLeft;           // 좌/우 컨트롤러
    public GameObject spawner;
    public GameObject gameplayUI;       // 플레이 중 UI
    public GameObject startUI;          // 시작화면 UI

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
                startUI.SetActive(true);
                gameplayUI.SetActive(false);
                sabersRight.SetActive(false);
                sabersLeft.SetActive(false);
                spawner.SetActive(false);
                foreach (Transform child in spawner.transform)
                {
                    child.gameObject.SetActive(false);
                }
                Time.timeScale = 0f; // 일시정지
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
