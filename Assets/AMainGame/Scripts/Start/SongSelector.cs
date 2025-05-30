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

    public Text titleText;
    public Text artistText;
    private int currentIndex = 0;
    public AudioSource previewPlayer; // Inspector�� AudioSource ����

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
}
