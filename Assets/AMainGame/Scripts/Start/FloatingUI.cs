using UnityEngine;

public class FloatingUI : MonoBehaviour
{
    public float amplitude = 100f;       // 위아래 움직이는 범위 (픽셀)
    public float frequency = 1f;        // 속도

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.localPosition = startPos + new Vector3(0f, offset, 0f);
    }
}
