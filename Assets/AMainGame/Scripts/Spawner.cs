using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] cubes;
    public Transform[] points;
    public float beat = 1;
    float timer;
    public int count;

    // Update is called once per frame
    void Update()
    {
        beat = Random.Range(0.3f, 1.5f);
        
        if (timer > beat)
        {
            // 큐브의 종류 인덱스, 큐브 어디를 히트할지 인덱스
            GameObject cube = Instantiate(cubes[Random.Range(0, 2)], points[Random.Range(0, count)]);
            cube.transform.localPosition = Vector3.zero; // 자기 중심점으로 한다.

            cube.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
            timer -= beat;
        }

        timer += Time.deltaTime;
    }
}
