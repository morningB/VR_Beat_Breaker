using UnityEngine;

public class GameStartUIController : MonoBehaviour
{
    public GameObject startPanel;     // 시작 화면 UI
    public GameObject gameplayUI;     // 점수 UI 등
    public GameObject sabers;         // Saber (좌우 컨트롤러 오브젝트)

    private bool hasStarted = false;

    void Start()
    {
        // 처음엔 게임 시작 UI만 활성화
        startPanel.SetActive(true);
        gameplayUI.SetActive(false);
        sabers.SetActive(false);
    }

    public void StartGame()
    {

        hasStarted = true;

        startPanel.SetActive(false);
        gameplayUI.SetActive(true);
        sabers.SetActive(true);

        //GameManager.Instance.StartGame(); // 상태 변경 및 점수 초기화
    }

    void Update()
    {
        
    }
}
