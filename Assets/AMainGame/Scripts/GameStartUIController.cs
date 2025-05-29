using UnityEngine;

public class GameStartUIController : MonoBehaviour
{
    public GameObject startPanel;     // ���� ȭ�� UI
    public GameObject gameplayUI;     // ���� UI ��
    public GameObject sabers;         // Saber (�¿� ��Ʈ�ѷ� ������Ʈ)

    private bool hasStarted = false;

    void Start()
    {
        // ó���� ���� ���� UI�� Ȱ��ȭ
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

        //GameManager.Instance.StartGame(); // ���� ���� �� ���� �ʱ�ȭ
    }

    void Update()
    {
        
    }
}
