using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SongSelector : MonoBehaviour
{
    public List<SongData> songs;  // Inspector���� �� ���� ����Ʈ ����
    public Image centerImage;
    public Image leftImage;
    public Image rightImage;
    public Button easy;
    public Button normal;
    public Button hard;

    public Text titleText;
    public Text artistText;
    private int currentIndex = 0;
    public AudioSource previewPlayer; // Inspector�� AudioSource ����

    public AudioSource sfxPlayer; // Inspector�� AudioSource ����
    public AudioClip nextAudio;
    public GameObject hitEffect;
    void Start()
    {
        UpdateUI();


    }
    public void SetDifficultyEasy()
    {
        sfxPlayer.PlayOneShot(nextAudio);
        SpawnEffectAtButton(GameObject.Find("Easy").GetComponent<Button>());
        SelectedSongHolder.selectedDifficulty = Difficulty.Easy;
        UpdateDifficultyVisuals();
    }

    public void SetDifficultyNormal()
    {
        sfxPlayer.PlayOneShot(nextAudio);
        SpawnEffectAtButton(GameObject.Find("Normal").GetComponent<Button>());
        SelectedSongHolder.selectedDifficulty = Difficulty.Normal;
        UpdateDifficultyVisuals();
    }

    public void SetDifficultyHard()
    {
        sfxPlayer.PlayOneShot(nextAudio);
        SpawnEffectAtButton(GameObject.Find("Hard").GetComponent<Button>());
        SelectedSongHolder.selectedDifficulty = Difficulty.Hard;
        UpdateDifficultyVisuals();
    }
    private void UpdateDifficultyVisuals()
    {
        Color selectedColor = new Color(1f, 1f, 1f, 1f); // 강조 색상 (예: 주황)
        Color normalColor = new Color(1f, 1f, 1f, 0f);  // 기본 색상

        // 버튼 배경 또는 텍스트 강조
        HighlightButton(easy, SelectedSongHolder.selectedDifficulty == Difficulty.Easy ? selectedColor : normalColor);
        HighlightButton(normal, SelectedSongHolder.selectedDifficulty == Difficulty.Normal ? selectedColor : normalColor);
        HighlightButton(hard, SelectedSongHolder.selectedDifficulty == Difficulty.Hard ? selectedColor : normalColor);
    }

    private void HighlightButton(Button btn, Color color)
    {
        Image bg = btn.GetComponent<Image>();
        if (bg != null) bg.color = color;
    }
    public void NextSong()
    {
        sfxPlayer.PlayOneShot(nextAudio);
        currentIndex = (currentIndex + 1) % songs.Count;
        SpawnEffectAtButton(GameObject.Find("Next").GetComponent<Button>());
        UpdateUI();
    }

    public void PreviousSong()
    {
        sfxPlayer.PlayOneShot(nextAudio);
        currentIndex = (currentIndex - 1 + songs.Count) % songs.Count;
        SpawnEffectAtButton(GameObject.Find("Previous").GetComponent<Button>());
        UpdateUI();
    }
    public void ConfirmSongSelection()
    {

        // ���õ� �� ������ �������� �ѱ��
        SelectedSongHolder.selectedSong = songs[currentIndex];
        Debug.Log("Main���� ����!!!!" + " ���� : " + SelectedSongHolder.selectedSong.ToString());

        // �� ��ȯ �Ǵ� ���� ����
        SceneManager.LoadScene("MainGame");

    }
    void UpdateUI()
    {
        int prevIndex = (currentIndex - 1 + songs.Count) % songs.Count;
        int nextIndex = (currentIndex + 1) % songs.Count;

        SongData current = songs[currentIndex];
        // �̹��� ����
        centerImage.sprite = current.jacketImage;
        leftImage.sprite = songs[prevIndex].jacketImage;
        rightImage.sprite = songs[nextIndex].jacketImage;
        // ���� �� ����
        titleText.text = current.title;
        artistText.text = current.artist;

        leftImage.rectTransform.localScale = Vector3.one * 0.7f;
        rightImage.rectTransform.localScale = Vector3.one * 0.7f;
        centerImage.rectTransform.localScale = Vector3.one;

        Color faded = new Color(1, 1, 1, 0.4f);
        Color normal = Color.white;

        leftImage.color = faded;
        rightImage.color = faded;
        centerImage.color = normal;


        if (previewPlayer.isPlaying)
        {
            previewPlayer.Stop(); // ���� �� ����
        }

        previewPlayer.clip = current.previewClip;
        previewPlayer.Play();
    }
    private void SpawnEffectAtButton(Button button)
    {
        // 버튼 위치 기준으로 월드 좌표 계산
        Vector3 spawnPos = button.transform.position;

        // 캔버스가 World Space라면 이걸 그대로 사용
        // 그렇지 않으면 Camera 기준으로 보정 필요
        GameObject effect = Instantiate(hitEffect, spawnPos, Quaternion.identity, button.transform);

        // 이펙트는 잠깐 후에 삭제
        Destroy(effect, 1.5f);
    }

}
