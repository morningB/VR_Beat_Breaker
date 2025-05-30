using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SongSelector : MonoBehaviour
{
    public List<SongData> songs;  // Inspector에서 곡 정보 리스트 연결
    public Image centerImage;
    public Image leftImage;
    public Image rightImage;

    public Text titleText;
    public Text artistText;
    private int currentIndex = 0;
    public AudioSource previewPlayer; // Inspector에 AudioSource 연결

    void Start()
    {
        UpdateUI();


    }

    public void NextSong()
    {
        currentIndex = (currentIndex + 1) % songs.Count;
        UpdateUI();
    }

    public void PreviousSong()
    {
        currentIndex = (currentIndex - 1 + songs.Count) % songs.Count;
        UpdateUI();
    }
    public void ConfirmSongSelection()
    {

        // 선택된 곡 정보를 전역으로 넘긴다
        SelectedSongHolder.selectedSong = songs[currentIndex];
        Debug.Log("Main으로 간다!!!!" + " 곡은 : " + SelectedSongHolder.selectedSong.ToString());

        // 씬 전환 또는 상태 변경
        SceneManager.LoadScene("MainGame");

    }

    void UpdateUI()
    {
        int prevIndex = (currentIndex - 1 + songs.Count) % songs.Count;
        int nextIndex = (currentIndex + 1) % songs.Count;

        SongData current = songs[currentIndex];
        // 이미지 설정
        centerImage.sprite = current.jacketImage;
        leftImage.sprite = songs[prevIndex].jacketImage;
        rightImage.sprite = songs[nextIndex].jacketImage;
        // 선택 곡 정보
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
            previewPlayer.Stop(); // 이전 곡 정지
        }

        previewPlayer.clip = current.previewClip;
        previewPlayer.Play();
    }
}
