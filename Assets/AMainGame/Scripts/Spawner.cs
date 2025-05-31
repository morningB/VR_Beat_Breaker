using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] cubes;
    public Transform[] points;
    float beat = 1;
    float timer;
    int count;
    int cubeCount;
    void Start()
    {
        switch (SelectedSongHolder.selectedDifficulty)
        {
            case Difficulty.Easy:
                beat = 3f;
                count = 2;
                cubeCount = 2;
                break;
            case Difficulty.Normal:
                beat = 2f;
                count = 4;
                cubeCount = 2;
                break;
            case Difficulty.Hard:
                beat = 1.5f;
                count = 8;
                cubeCount = 4;
                break;
        }
    }
    void Update()
    {
        beat = Random.Range(0.3f, beat);
        
        if (timer > beat)
        {
            // ť���� ���� �ε���, ť�� ��� ��Ʈ���� �ε���
            GameObject cube = Instantiate(cubes[Random.Range(0, cubeCount)], points[Random.Range(0, count)]);
            cube.transform.localPosition = Vector3.zero; // �ڱ� �߽������� �Ѵ�.

            cube.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
            timer -= beat;
        }

        timer += Time.deltaTime;
    }
}
