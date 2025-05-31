using UnityEngine;

public enum Difficulty
{
    Easy,
    Normal,
    Hard
}

[System.Serializable]
public class SongData
{
    public string title;
    public string artist;
    public Sprite jacketImage;
    public AudioClip previewClip;
}
